using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VolcanoFinder.API.Models.DTOs;
using VolcanoFinder.API.Services;

namespace VolcanoFinder.API.Controllers
{
    [Route("api/regions")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IVolcanoFinderRepository _volcanoFinderRepository;
        private readonly IMapper _mapper;

        public RegionsController(IVolcanoFinderRepository volcanoFinderRepository, IMapper mapper)
        {
            _volcanoFinderRepository = volcanoFinderRepository ?? throw new ArgumentNullException(nameof(volcanoFinderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetRegions(bool includeVolcanoes = false)
        {
            var regionEntities = await _volcanoFinderRepository.GetRegionsAsync(includeVolcanoes);

            if(includeVolcanoes)
                return Ok(_mapper.Map<IEnumerable<RegionDto>>(regionEntities));

            return Ok(_mapper.Map<IEnumerable<RegionWithoutVolcanoesDto>>(regionEntities));
        }

        [HttpGet("{regionId}")]
        public async Task<IActionResult> GetRegion(int regionId, bool includeVolcanoes = false)
        {
            var regionEntity = await _volcanoFinderRepository.GetRegionAsync(regionId, includeVolcanoes);

            if(regionEntity is null)
                return NotFound();

            if (includeVolcanoes)
                return Ok(_mapper.Map<RegionDto>(regionEntity));

            return Ok(_mapper.Map<RegionWithoutVolcanoesDto>(regionEntity));
        }
    }
}
