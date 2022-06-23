using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using VolcanoFinder.API.Models.DTOs;
using VolcanoFinder.API.Services;

namespace VolcanoFinder.API.Controllers
{
    [Route("api/v:{version:apiVersion}/regions")]
    [ApiController]
    [ApiVersion("1.0")]
    public class RegionsController : ControllerBase
    {
        private readonly IVolcanoFinderRepository _volcanoFinderRepository;
        private readonly IMapper _mapper;
        const int maxPageSize = 20;

        public RegionsController(IVolcanoFinderRepository volcanoFinderRepository, IMapper mapper)
        {
            _volcanoFinderRepository = volcanoFinderRepository ?? throw new ArgumentNullException(nameof(volcanoFinderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get all regions
        /// </summary>
        /// <param name="includeVolcanoes">Whether or not to include the volcanos</param>
        /// <param name="pageNumber">The number of the page to get</param>
        /// <param name="pageSize">The size of the page to get (max. value is 20)</param>
        /// <returns>An IActionResult</returns>
        [HttpGet]
        public async Task<IActionResult> GetRegions(bool includeVolcanoes = false, int pageNumber = 1 , int pageSize = 10)
        {
            if(pageSize > maxPageSize)
                pageSize = maxPageSize;

            var (regionEntities, paginationMetadata) = await _volcanoFinderRepository.GetRegionsAsync(includeVolcanoes, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            if(includeVolcanoes)
                return Ok(_mapper.Map<IEnumerable<RegionDto>>(regionEntities));

            return Ok(_mapper.Map<IEnumerable<RegionWithoutVolcanoesDto>>(regionEntities));
        }

        /// <summary>
        /// Get the region with the specified regionId
        /// </summary>
        /// <param name="regionId">The id of the region to get</param>
        /// <param name="includeVolcanoes">Whether or not to include the volcanos</param>
        /// <returns>IActionResult</returns>
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
