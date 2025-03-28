using AutoMapper;
using PetTracking.DTOs;
using PetTracking.Models;

namespace PetTracking.AutoMapper
{
    public class OwnerMappingProfiles : Profile
    {
        public OwnerMappingProfiles() 
        {
            CreateMap<Owner, GetOwnerDTO>();

            CreateMap<CreateOwnerDTO, Owner>();

            CreateMap<Owner, EditOwnerDTO>();
            CreateMap<EditOwnerDTO, Owner>();
            
        }
    }
}
