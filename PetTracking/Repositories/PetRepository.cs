using Microsoft.EntityFrameworkCore;
using PetTracking.Data;
using PetTracking.Models;

namespace PetTracking.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly AppDbContext _context;
        public PetRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddPetAsync(Pet pet)
        {
            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePetAsync(Pet pet)
        {
            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Pet>> GetAllPetsAsync()
        {
            return await _context.Pets.ToListAsync();
        }

        public async Task<Pet?> GetPetByIdAsync(int id)
        {
            return await _context.Pets.FirstOrDefaultAsync(p => p.PetId == id);
        }

        public async Task<IEnumerable<Pet>> GetAllPetsWithOwnersAsync()
        {
            return await _context.Pets
                .Include(p => p.Owner)
                .ToListAsync();
        }

        public async Task<Pet?> GetAllInfoForPetAsync(int id)
        {
            return await _context.Pets
                .Include(p => p.PetVaccines)
                .ThenInclude(pv => pv.Vaccine)
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(p => p.PetId == id);
        }

        public async Task UpdatePetAsync(Pet pet)
        {
            _context.Pets.Update(pet);
            await _context.SaveChangesAsync();
        }

        
    }
}
