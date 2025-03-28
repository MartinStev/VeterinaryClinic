using Microsoft.EntityFrameworkCore;
using PetTracking.Data;
using PetTracking.Models;

namespace PetTracking.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly AppDbContext _context;

        public OwnerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddOwnerAsync(Owner owner)
        {
            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOwnerAsync(Owner owner)
        {
            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Owner>> GetAllOwnersAsync()
        {
            return await _context.Owners.ToListAsync();
        }

        public async Task<Owner?> GetOwnerByIdAsync(int id)
        {
            return await _context.Owners.FirstOrDefaultAsync(ow => ow.OwnerId == id);
        }

        public async Task<Owner?> GetOwnerWithPetsAsync(int id)
        {
            return await _context.Owners
                .Include(ow => ow.Pets)
                .FirstOrDefaultAsync(ow => ow.OwnerId == id);
        }

        public async Task UpdateOwnerAsync(Owner owner)
        {
            _context.Owners.Update(owner);
            await _context.SaveChangesAsync(); 
        }
    }
}
