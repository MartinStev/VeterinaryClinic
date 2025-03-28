using PetTracking.Models;

namespace PetTracking.Repositories
{
    public interface IOwnerRepository
    {
        Task<List<Owner>> GetAllOwnersAsync();
        Task<Owner?> GetOwnerByIdAsync(int id);
        Task<Owner?> GetOwnerWithPetsAsync(int id);
        Task AddOwnerAsync(Owner owner);
        Task DeleteOwnerAsync(Owner owner);
        Task UpdateOwnerAsync(Owner owner);
    }
}
