using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Caching.Memory;
using PetTracking.DTOs;
using PetTracking.Models;
using PetTracking.Repositories;
using System.Collections.Generic;
using System.Drawing.Printing;


namespace PetTracking.Controllers
{
    [Authorize]
    public class PetController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IVaccineRepository _vaccineRepository;
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        public PetController(IPetRepository petRepository, IOwnerRepository ownerRepository, 
            IVaccineRepository vaccineRepository, IMapper mapper, IMemoryCache memoryCache)
        {
            _petRepository = petRepository;
            _ownerRepository = ownerRepository;
            _vaccineRepository = vaccineRepository;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }
        
        public async Task<IActionResult> GetAllPets(int page = 1, int pageSize = 6) 
        {
            const string cacheKey = "AllPetsCacheKey";

            if (!_memoryCache.TryGetValue(cacheKey, out List<GetPetDTO> petsGet))
            {
                var pets = await _petRepository.GetAllPetsWithOwnersAsync();
                petsGet = _mapper.Map<List<GetPetDTO>>(pets);
     
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5)) 
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _memoryCache.Set(cacheKey, petsGet, cacheOptions);
            }

            int totalPets = petsGet.Count;

            int totalPages = (int)Math.Ceiling((double)totalPets / pageSize);

            var pagedPets = petsGet
            .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;

            return View(pagedPets);
        }

        public async Task<IActionResult> Details(int id, int page)
        {
            var petInfo = await _petRepository.GetAllInfoForPetAsync(id);
            if(petInfo == null)
            {
                return NotFound();
            }

            ViewBag.CurrentPage = page; 
            return View(petInfo);
        }

        public async Task<IActionResult> Create()
        {
            var owners = await _ownerRepository.GetAllOwnersAsync();
            var vaccines = await _vaccineRepository.GetAllVaccinesAsync();

            ViewBag.Owners = new SelectList(owners, "OwnerId", "Name");
            ViewBag.Vaccines = new MultiSelectList(vaccines, "VaccineId", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePetDTO createPet, string hasVaccines)
        {

            if (ModelState.IsValid)
            {
                var domainPet = _mapper.Map<Pet>(createPet);

                if (createPet.VaccineIds != null && createPet.VaccineIds.Any())
                {
                    domainPet.PetVaccines = createPet.VaccineIds
                        .Select(vaccineId => new PetVaccine { VaccineId = vaccineId, Pet = domainPet })
                        .ToList();
                }

                await _petRepository.AddPetAsync(domainPet);

                if (createPet.PetImage != null && createPet.PetImage.Length > 0)
                {
                    var imagePath = Path.Combine("wwwroot/images", $"{domainPet.PetId}_.jpg");
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await createPet.PetImage.CopyToAsync(stream);
                    }
                }

                _memoryCache.Remove("AllPetsCacheKey");

                return RedirectToAction("GetAllPets");
            }

            var owners = await _ownerRepository.GetAllOwnersAsync();
            var vaccines = await _vaccineRepository.GetAllVaccinesAsync();

            ViewBag.Owners = new SelectList(owners, "OwnerId", "Name");
            ViewBag.Vaccines = new MultiSelectList(vaccines, "VaccineId", "Name");
            ViewBag.HasVaccines = hasVaccines;          

            return View(createPet);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id, int page)
        {
            var petToDelete = await _petRepository.GetAllInfoForPetAsync(id);
            if(petToDelete == null)
            {
                return NotFound();
            }
            ViewBag.CurrentPage = page; 
            return View(petToDelete);
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id, int page)
        {
            var petToDelete = await _petRepository.GetAllInfoForPetAsync(id);
            _petRepository.DeletePetAsync(petToDelete);

            _memoryCache.Remove("AllPetsCacheKey");

            return RedirectToAction("GetAllPets", new { page = page });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, int page)
        {
            var domainPet = await _petRepository.GetAllInfoForPetAsync(id);

            var petToEdit = _mapper.Map<EditPetDTO>(domainPet);
            petToEdit.VaccineIds = domainPet.PetVaccines.Select(pv => pv.VaccineId).ToList();


            var owners = await _ownerRepository.GetAllOwnersAsync();
            var allVaccines = await _vaccineRepository.GetAllVaccinesAsync();

            var availableVaccines = allVaccines.Where(v => !domainPet.PetVaccines.Any(pv => pv.VaccineId == v.VaccineId)).ToList();

            ViewBag.Owners = new SelectList(owners, "OwnerId", "Name");
            ViewBag.Vaccines = new MultiSelectList(availableVaccines, "VaccineId", "Name");
            ViewBag.CurrentPage = page; 

            return View(petToEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditPetDTO dto, int page)
        {

            if (ModelState.IsValid)
            {
                var pet = await _petRepository.GetAllInfoForPetAsync(dto.PetId);
                if (pet == null)
                {
                    return NotFound();
                }

                _mapper.Map<Pet>(dto);
                
                pet.Name = dto.Name;
                pet.Age = dto.Age;
                pet.OwnerId = dto.OwnerId;

                foreach (var vaccineId in dto.VaccineIds)
                {
                    pet.PetVaccines.Add(new PetVaccine
                    {
                        PetId = pet.PetId,
                        VaccineId = vaccineId
                    });
                }

                await _petRepository.UpdatePetAsync(pet);

                _memoryCache.Remove("AllPetsCacheKey");

                return RedirectToAction("GetAllPets", new { page = page });
            }

            var owners = await _ownerRepository.GetAllOwnersAsync();
            var vaccines = await _vaccineRepository.GetAllVaccinesAsync();

            ViewBag.Owners = new SelectList(owners, "OwnerId", "Name");
            ViewBag.Vaccines = new MultiSelectList(vaccines, "VaccineId", "Name");
            return View(dto);
        }
    }
}
