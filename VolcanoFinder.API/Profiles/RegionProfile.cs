using AutoMapper;
using VolcanoFinder.API.Models.DTOs;
using VolcanoFinder.API.Models.Entities;

namespace VolcanoFinder.API.Profiles
{
    public class RegionProfile : Profile
    {
        public RegionProfile()
        {
            CreateMap<Region, RegionDto>();
            CreateMap<Region, RegionWithoutVolcanoesDto>();
        }
    }
}
