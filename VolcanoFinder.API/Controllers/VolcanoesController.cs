﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using VolcanoFinder.API.Models.DTOs;
using VolcanoFinder.API.Models.Entities;
using VolcanoFinder.API.Models.Metadata;
using VolcanoFinder.API.Services;

namespace VolcanoFinder.API.Controllers
{
    [Route("api/v:{version:apiVersion}/regions/{regionId}/volcanoes")]
    [ApiController]
    [ApiVersion("1.0")]
    public class VolcanoesController : ControllerBase
    {
        private readonly IVolcanoFinderRepository _volcanoFinderRepository;
        private readonly IMapper _mapper;
        const int maxPageSize = 20;

        public VolcanoesController(IVolcanoFinderRepository volcanoFinderRepository, IMapper mapper)
        {
            _volcanoFinderRepository = volcanoFinderRepository ?? throw new ArgumentNullException(nameof(volcanoFinderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets the volcanoes from the region with the specified regionId
        /// </summary>
        /// <param name="regionId">The id of the region to get the volcanos from</param>
        /// <param name="active">Filters by the Active attribute</param>
        /// <param name="searchQuery">Returns results containing the search query in their Name, Description or CountryAlpha2</param>
        /// <param name="pageNumber">The number of the page to get</param>
        /// <param name="pageSize">The size of the page to get (max. value is 20)</param>
        /// <returns>ActionResult<IEnumerable<VolcanoDto>></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VolcanoDto>>> GetVolcanoesFromRegion(int regionId, bool? active, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (!await _volcanoFinderRepository.RegionExistsAsync(regionId))
                return NotFound();

            var (volcanoEntities, paginationMetadata) = await _volcanoFinderRepository.GetVolcanoesFromRegionAsync(regionId, active, searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<VolcanoDto>>(volcanoEntities));
        }

        /// <summary>
        /// Gets the volcano with the specified volcanoId from the region with the specified regionId
        /// </summary>
        /// <param name="regionId">The id of the region to get the volcano from</param>
        /// <param name="volcanoId">The id of the volcano to get</param>
        /// <returns>ActionResult<VolcanoDto></returns>
        [HttpGet("{volcanoId}", Name = "GetVolcanoFromRegion")]
        public async Task<ActionResult<VolcanoDto>> GetVolcanoFromRegion(int regionId, int volcanoId)
        {
            if (!await _volcanoFinderRepository.RegionExistsAsync(regionId))
                return NotFound();

            var volcanoEntity = await _volcanoFinderRepository.GetVolcanoFromRegionAsync(regionId, volcanoId);

            if (volcanoEntity is null)
                return NotFound();

            return Ok(_mapper.Map<VolcanoDto>(volcanoEntity));
        }

        /// <summary>
        /// Adds a volcano to the region with the specified regionId
        /// </summary>
        /// <param name="regionId">The id of the region to add the volcano to</param>
        /// <param name="volcanoForCreationDto">The volcanoForCreationDto to add</param>
        /// <returns>ActionResult<VolcanoDto></returns>
        [HttpPost]
        public async Task<ActionResult<VolcanoDto>> AddVolcanoToRegion(int regionId, VolcanoForCreationDto volcanoForCreationDto)
        {
            if (!await _volcanoFinderRepository.RegionExistsAsync(regionId))
                return NotFound();

            var volcanoEntity = _mapper.Map<Volcano>(volcanoForCreationDto);

            await _volcanoFinderRepository.AddVolcanoToRegionAsync(regionId, volcanoEntity);

            var volcanoDto = _mapper.Map<VolcanoDto>(volcanoEntity);

            return CreatedAtRoute("GetVolcanoFromRegion", new
            {
                regionId = regionId,
                volcanoId = volcanoDto.Id
            },
            volcanoDto);
        }

        /// <summary>
        ///  Updates the volcano with the specified volcanoId in the region with the specified regionId with the provided volcanoForUpdateDto
        /// </summary>
        /// <param name="regionId">The id of the region containing the volcano to be updated</param>
        /// <param name="volcanoId">The id of the volcano to update</param>
        /// <param name="volcanoForUpdateDto">The volcanoForCreationDto to update the volcano with</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{volcanoId}")]
        public async Task<IActionResult> UpdateVolcano(int regionId, int volcanoId, VolcanoForUpdateDto volcanoForUpdateDto)
        {
            if (!await _volcanoFinderRepository.RegionExistsAsync(regionId))
                return NotFound();

            var volcanoEntity = await _volcanoFinderRepository.GetVolcanoFromRegionAsync(regionId, volcanoId);

            if (volcanoEntity is null)
                return NotFound();

            _mapper.Map(volcanoForUpdateDto, volcanoEntity);

            await _volcanoFinderRepository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Patches the volcano with the specified volcanoId in the region with the specified regionId with the provided patchDocument
        /// </summary>
        /// <param name="regionId">The id of the region containing the volcano to be patched</param>
        /// <param name="volcanoId">The id of the volcano to be patched</param>
        /// <param name="patchDocument">The patchDocument to patch the volcano with</param>
        /// <returns>IActionResult</returns>
        [HttpPatch("{volcanoId}")]
        public async Task<IActionResult> PartiallyUpdateVolcano(int regionId, int volcanoId, JsonPatchDocument<VolcanoForUpdateDto> patchDocument)
        {
            if (!await _volcanoFinderRepository.RegionExistsAsync(regionId))
                return NotFound();

            var volcanoEntity = await _volcanoFinderRepository.GetVolcanoFromRegionAsync(regionId, volcanoId);

            if (volcanoEntity is null)
                return NotFound();

            var volcanoDtoToPatch = _mapper.Map<VolcanoForUpdateDto>(volcanoEntity);

            patchDocument.ApplyTo(volcanoDtoToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!TryValidateModel(volcanoDtoToPatch))
                return BadRequest();

            _mapper.Map(volcanoDtoToPatch, volcanoEntity);

            await _volcanoFinderRepository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes the volcano with the specified volcanoId in the region with the specified regionId
        /// </summary>
        /// <param name="regionId">The id of the region containing the volcano to be deleted</param>
        /// <param name="volcanoId">The id of the volcano to be deleted</param>
        /// <returns>IActionResult</returns>
        [HttpDelete("{volcanoId}")]
        public async Task<IActionResult> DeleteVolcano(int regionId, int volcanoId)
        {
            if (!await _volcanoFinderRepository.RegionExistsAsync(regionId))
                return NotFound();

            var volcanoEntity = await _volcanoFinderRepository.GetVolcanoFromRegionAsync(regionId, volcanoId);

            if (volcanoEntity is null)
                return NotFound();

            _volcanoFinderRepository.DeleteVolcano(volcanoEntity);

            await _volcanoFinderRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
