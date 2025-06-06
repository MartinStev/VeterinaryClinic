﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PetTracking.DTOs;
using PetTracking.Models;
using PetTracking.Repositories;
using System.Security.Claims;

namespace PetTracking.Controllers
{
    [Authorize]
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper, IMemoryCache memoryCache)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public IActionResult TestRoles()
        {
            var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            return Json(roles);
        }

        public async Task<IActionResult> GetAllOwners()
        {
            const string cacheKey = "AllOwnersCacheKey";

            if (!_memoryCache.TryGetValue(cacheKey, out List<GetOwnerDTO> ownersGet))
            {
                var owners = await _ownerRepository.GetAllOwnersAsync();
                ownersGet = _mapper.Map<List<GetOwnerDTO>>(owners);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1)); 

                _memoryCache.Set(cacheKey, ownersGet, cacheOptions);
            }
            
            return View(ownersGet);
        }

        public async Task<IActionResult> Details(int id)
        {
            var ownerWithPets = await _ownerRepository.GetOwnerWithPetsAsync(id);
            return View(ownerWithPets);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateOwnerDTO owner)
        {
            if (ModelState.IsValid)
            {
                var domainOwner = _mapper.Map<Owner>(owner);

                await _ownerRepository.AddOwnerAsync(domainOwner);

                _memoryCache.Remove("AllOwnersCacheKey");

                return RedirectToAction("GetAllOwners");
            }
            return View(owner);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var owner = await _ownerRepository.GetOwnerByIdAsync(id);
            if(owner == null)
            {
                return NotFound();
            }

            var editOwner = _mapper.Map<EditOwnerDTO>(owner);
            return View(editOwner);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditOwnerDTO editOwner)
        {
            if(ModelState.IsValid)
            {
                var owner = _mapper.Map<Owner>(editOwner);
                await _ownerRepository.UpdateOwnerAsync(owner);

                _memoryCache.Remove("AllOwnersCacheKey");

                return RedirectToAction("GetAllOwners");
            }
            return View(editOwner);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var owner = await _ownerRepository.GetOwnerByIdAsync(id);
            if(owner == null)
            {
                return NotFound();
            }

            await _ownerRepository.DeleteOwnerAsync(owner);

            _memoryCache.Remove("AllOwnersCacheKey");

            _memoryCache.Remove("AllPetsCacheKey");

            return RedirectToAction("GetAllOwners");
        }
    }
}
