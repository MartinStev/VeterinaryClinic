using System.ComponentModel.DataAnnotations;

namespace PetTracking.Models
{
    public class Vaccine
    {
        public int VaccineId { get; set; }
        public string Name { get; set; }

        public Boolean IsDeleted { get; set; } = false;

        // Many-to-Many: Vaccine can belong to multiple Pets
        public ICollection<PetVaccine> PetVaccines { get; set; }
    }
}