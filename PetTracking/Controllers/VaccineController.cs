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
                var vaccines = await _vaccineRepository.GetAllVaccinesAsync();

                var activeVaccines = vaccines.Where(v => !v.IsDeleted).ToList();

                vaccineGet = _mapper.Map<List<GetVaccineDTO>>(activeVaccines);

                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(1) 
                };

                _memoryCache.Set(cacheKey, vaccineGet, cacheOptions);
            }

            return View(vaccineGet);
        }


        public async Task<IActionResult> Details(int id)
        {
            var vaccineWithPets = await _vaccineRepository.GetVaccineWithPetsAsync(id);
            if (vaccineWithPets == null)
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
            if (ModelState.IsValid)
            {
                var existingVaccine = await _vaccineRepository.GetVaccineByNameAsync(createVaccine.Name);

                if (existingVaccine != null && existingVaccine.IsDeleted)
                {
                    existingVaccine.IsDeleted = false;
                    await _vaccineRepository.UpdateVaccineAsync(existingVaccine);

                    _memoryCache.Remove("vaccines_list");

                    return RedirectToAction("GetAllVaccines");
                }

                var domainVaccine = _mapper.Map<Vaccine>(createVaccine);

                try
                {
                    if (existingVaccine != null)
                    {
                        if (existingVaccine.Name != domainVaccine.Name)
                        {
                            await _vaccineRepository.AddVaccineAsync(domainVaccine);

                            _memoryCache.Remove("vaccines_list");
                            return RedirectToAction("GetAllVaccines");
                        }
                        else
                            throw new Exception($"Vaccine {createVaccine.Name} already exists!");
                    }
                    else
                    {
                        await _vaccineRepository.AddVaccineAsync(domainVaccine);

                        _memoryCache.Remove("vaccines_list");
                        return RedirectToAction("GetAllVaccines");
                    }

                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", ex.Message);
                    return View(createVaccine);
                }

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
            var existingVaccine = await _vaccineRepository.GetVaccineByNameAsync(editVaccine.Name);

            if (ModelState.IsValid)
            {
                try
                {
                    if (existingVaccine == null)
                    {
                        var vaccine = _mapper.Map<Vaccine>(editVaccine);
                        await _vaccineRepository.UpdateVaccineAsync(vaccine);

                        _memoryCache.Remove("vaccines_list");

                        return RedirectToAction("GetAllVaccines");
                    }
                    else
                        throw new Exception($"Vaccine {editVaccine.Name} already exists. Vaccines in the system cannot have identical names!");
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", ex.Message);
                    return View(editVaccine);
                }
            }
            return View(editVaccine);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var vaccine = await _vaccineRepository.GetVaccineByIdAsync(id);
            if (vaccine == null)
            {
                return NotFound();
            }

            vaccine.IsDeleted = true;
            await _vaccineRepository.UpdateVaccineAsync(vaccine);

            _memoryCache.Remove("vaccines_list");

            return RedirectToAction("GetAllVaccines");
        }
    }
}