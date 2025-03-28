using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PetTracking.DTOs;
using PetTracking.Models;
using PetTracking.Repositories;

namespace PetTracking.Controllers
{
    [Authorize]
    public class VaccineController : Controller
    {
        private readonly IVaccineRepository _vaccineRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        public VaccineController(IVaccineRepository vaccineRepository, IMapper mapper, IMemoryCache memoryCache)
        {
            _vaccineRepository = vaccineRepository;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> GetAllVaccines()
        {
            const string cacheKey = "vaccines_list";

            if (!_memoryCache.TryGetValue(cacheKey, out List<GetVaccineDTO> vaccineGet))
            {
                // Cache miss: fetch from DB
                var vaccines = await _vaccineRepository.GetAllVaccinesAsync();
                vaccineGet = _mapper.Map<List<GetVaccineDTO>>(vaccines);

                // Cache the data with absolute and sliding expiration
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5), // Data expires after 5 minutes
                    SlidingExpiration = TimeSpan.FromMinutes(1) // Refreshes expiration if accessed within 1 minutes
                };

                _memoryCache.Set(cacheKey, vaccineGet, cacheOptions);
            }

            return View(vaccineGet);
        }


        public async Task<IActionResult> Details(int id)
        {
            var vaccineWithPets = await _vaccineRepository.GetVaccineWithPetsAsync(id);
            if(vaccineWithPets == null)
            {
                return NotFound();
            }
            return View(vaccineWithPets);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateVaccineDTO createVaccine)
        {
            if(ModelState.IsValid)
            {
                var domainVaccine = _mapper.Map<Vaccine>(createVaccine);
                await _vaccineRepository.AddVaccineAsync(domainVaccine);

                _memoryCache.Remove("vaccines_list"); 

                return RedirectToAction("GetAllVaccines");
            }
            return View(createVaccine);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var vaccine = await _vaccineRepository.GetVaccineByIdAsync(id);
            if (vaccine == null)
            {
                return NotFound();
            }
            var editVaccine = _mapper.Map<EditVaccineDTO>(vaccine);
            return View(editVaccine);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditVaccineDTO editVaccine)
        {
            if (ModelState.IsValid)
            {
                var vaccine = _mapper.Map<Vaccine>(editVaccine);
                await _vaccineRepository.UpdateVaccineAsync(vaccine);

                _memoryCache.Remove("vaccines_list"); 

                return RedirectToAction("GetAllVaccines");
            }
            return View(editVaccine);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var vaccine = await _vaccineRepository.GetVaccineByIdAsync(id);
            if(vaccine == null)
            {
                return NotFound();
            }

            await _vaccineRepository.DeleteVaccineAsync(vaccine);

            _memoryCache.Remove("vaccines_list"); 

            return RedirectToAction("GetAllVaccines");
        }
     
    }
}
