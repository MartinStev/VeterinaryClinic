using System.ComponentModel.DataAnnotations;

namespace PetTracking.Models
{
    public class Pet
    {
        public int PetId { get; set; }

        public string Name { get; set; }

        [Range(0, 50, ErrorMessage = "Pet age must be between 0 and 50.")]
        public int Age { get; set; }


        public int OwnerId { get; set; } //FK
        public Owner Owner { get; set; } //Navigational property

        // Many-to-Many: Pet can have multiple Vaccines
        public ICollection<PetVaccine> PetVaccines { get; set; }
    }
}
