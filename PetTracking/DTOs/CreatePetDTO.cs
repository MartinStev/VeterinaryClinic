using PetTracking.Models;
using System.ComponentModel.DataAnnotations;

namespace PetTracking.DTOs
{
    public class CreatePetDTO
    {
        public string Name { get; set; }

        [Range(0, 50, ErrorMessage = "Pet age must be between 0 and 50.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Please select an owner.")]
        public int OwnerId { get; set; }

        // Optional list of Vaccine IDs
        public List<int> VaccineIds { get; set; } = new List<int>();

        [Required(ErrorMessage = "Please upload an image.")]
        public IFormFile PetImage { get; set; }
    }
}
