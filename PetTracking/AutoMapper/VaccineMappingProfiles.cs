using AutoMapper;
using PetTracking.DTOs;
using PetTracking.Models;

namespace PetTracking.AutoMapper
{
    public class VaccineMappingProfiles : Profile
    {
        public VaccineMappingProfiles() 
        {
            CreateMap<Vaccine, GetVaccineDTO>();

            CreateMap<GetVaccineDTO, Vaccine>();

            CreateMap<CreateVaccineDTO, Vaccine>();

            CreateMap<Vaccine, EditVaccineDTO>();
            CreateMap<EditVaccineDTO, Vaccine>();
        }
    }
}
