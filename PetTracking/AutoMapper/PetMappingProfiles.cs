using AutoMapper;
using PetTracking.DTOs;
using PetTracking.Models;

namespace PetTracking.AutoMapper
{
    public class PetMappingProfiles : Profile
    {
        public PetMappingProfiles()
        {
            CreateMap<Pet, GetPetDTO>();
            CreateMap<CreatePetDTO, Pet>();

            CreateMap<Pet, EditPetDTO>();
            CreateMap<EditPetDTO, Pet>();

        }
    }
  
}
