using Microsoft.EntityFrameworkCore;
using PetTracking.Data;
using PetTracking.Models;

namespace PetTracking.Repositories
{
    public class VaccineRepository : IVaccineRepository
    {
        private readonly AppDbContext _context;
        public VaccineRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddVaccineAsync(Vaccine vaccine)
        {
            //await _context.Vaccines.AddAsync(vaccine);
             _context.Vaccines.Add(vaccine);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVaccineAsync(Vaccine vaccine)
        {
            _context.Vaccines.Remove(vaccine);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Vaccine>> GetAllVaccinesAsync()
        {
            return await _context.Vaccines.ToListAsync();
        }

        public async Task<Vaccine?> GetVaccineByIdAsync(int id)
        {
            return await _context.Vaccines.FirstOrDefaultAsync(v => v.VaccineId == id);
        }

        public async Task<Vaccine?> GetVaccineWithPetsAsync(int id)
        {
            return await _context.Vaccines
                .Include(v => v.PetVaccines)
                .ThenInclude(pv => pv.Pet) // Include Pet details
                .FirstOrDefaultAsync(v => v.VaccineId == id);
        }

        public async Task UpdateVaccineAsync(Vaccine vaccine)
        {
            _context.Vaccines.Update(vaccine);
            await _context.SaveChangesAsync();
        }
    }
}
