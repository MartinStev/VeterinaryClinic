using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace PetTracking.Models
{
    public class Owner
    {
        public int OwnerId { get; set; }

        public string Name { get; set; }
        
        public string Surname { get; set; }

        [Range(18, 100, ErrorMessage = "Owner age must be between 18 and 100.")]
        public int Age { get; set; }

        // One-to-Many: One Owner can have multiple Pets
        public ICollection<Pet> Pets { get; set; }

        public string GetFullName()
        {
            return Name + " " + Surname;
        }
    }
}
