using AutoMapper;
using VolcanoFinder.API.Models.DTOs;
using VolcanoFinder.API.Models.Entities;

namespace VolcanoFinder.API.Profiles
{
    public class VolcanoProfile : Profile
    {
        public VolcanoProfile()
        {
            CreateMap<Volcano, VolcanoDto>();
            CreateMap<VolcanoForCreationDto, Volcano>();
            CreateMap<VolcanoForUpdateDto, Volcano>();
        }
    }
}
