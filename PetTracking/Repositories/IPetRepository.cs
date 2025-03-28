using PetTracking.Models;

namespace PetTracking.Repositories
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> GetAllPetsAsync();
        Task<IEnumerable<Pet>> GetAllPetsWithOwnersAsync();
        Task<Pet?> GetPetByIdAsync(int id);
        Task<Pet?> GetAllInfoForPetAsync(int id); // Get pet with associated vaccines and owners
        Task AddPetAsync(Pet pet);
        Task DeletePetAsync(Pet pet);
        Task UpdatePetAsync(Pet pet);
    }
}
