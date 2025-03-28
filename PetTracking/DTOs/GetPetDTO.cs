using PetTracking.Models;
using System.ComponentModel.DataAnnotations;

namespace PetTracking.DTOs
{
    public class GetPetDTO
    {
        public int PetId { get; set; }

        public string Name { get; set; }

        [Range(0, 50, ErrorMessage = "Pet age must be between 0 and 50.")]
        public int Age { get; set; }

        public Owner Owner { get; set; }
    }
}
