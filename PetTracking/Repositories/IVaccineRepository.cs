using PetTracking.Models;

namespace PetTracking.Repositories
{
    public interface IVaccineRepository
    {
        //ili Task<List<Vaccine>>
        Task<IEnumerable<Vaccine>> GetAllVaccinesAsync();
        Task<Vaccine?> GetVaccineByIdAsync(int id);
        Task<Vaccine?> GetVaccineWithPetsAsync(int id); // Get vaccine with associated pets
        Task AddVaccineAsync(Vaccine vaccine);
        Task DeleteVaccineAsync(Vaccine vaccine);
        Task UpdateVaccineAsync(Vaccine vaccine);
        Task GetVaccineByNameAsync(Vaccine vaccine);
        Task<Vaccine> GetVaccineByNameAsync(string name);
    }
}