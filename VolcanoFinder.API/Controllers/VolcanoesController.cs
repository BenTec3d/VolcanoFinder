﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VolcanoFinder.API.Models.DTOs;
using VolcanoFinder.API.Services;

namespace VolcanoFinder.API.Controllers
{
    [Route("api/regions/{regionId}/volcanoes")]
    [ApiController]
    public class VolcanoesController : ControllerBase
    {
        private readonly IVolcanoFinderRepository _volcanoFinderRepository;
        private readonly IMapper _mapper;

        public VolcanoesController(IVolcanoFinderRepository volcanoFinderRepository, IMapper mapper)
        {
            _volcanoFinderRepository = volcanoFinderRepository ?? throw new ArgumentNullException(nameof(volcanoFinderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetVolcanoesFromRegion(int regionId)
        {
            if (!await _volcanoFinderRepository.RegionExistsAsync(regionId))
                return NotFound();

            var volcanoEntities = await _volcanoFinderRepository.GetVolcanoesFromRegionAsync(regionId);

            return Ok(_mapper.Map<IEnumerable<VolcanoDto>>(volcanoEntities));
        }

        [HttpGet("{volcanoId}")]
        public async Task<IActionResult> GetVolcanoFromRegion(int regionId, int volcanoId)
        {
            if (!await _volcanoFinderRepository.RegionExistsAsync(regionId))
                return NotFound();

            var volcanoEntity = await _volcanoFinderRepository.GetVolcanoFromRegionAsync(regionId, volcanoId);

            if(volcanoEntity is null)
                return NotFound();

            return Ok(_mapper.Map<VolcanoDto>(volcanoEntity));
        }
    }
}
