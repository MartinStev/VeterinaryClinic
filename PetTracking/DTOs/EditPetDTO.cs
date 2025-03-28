using PetTracking.Models;
using System.ComponentModel.DataAnnotations;

namespace PetTracking.DTOs
{
    public class EditPetDTO
    {
        public int PetId { get; set; }

        public string Name { get; set; }

        [Range(0, 50, ErrorMessage = "Pet age must be between 0 and 50.")]
        public int Age { get; set; }


        public int OwnerId { get; set; }
        public List<int> VaccineIds { get; set; } = new List<int>();
        
    }
}
