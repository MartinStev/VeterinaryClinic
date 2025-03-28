﻿using System.ComponentModel.DataAnnotations;

namespace PetTracking.DTOs
{
    public class GetOwnerDTO
    {
        public int OwnerId { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        [Range(18, 100, ErrorMessage = "Owner age must be between 18 and 100.")]
        public int Age { get; set; }
    }
}
