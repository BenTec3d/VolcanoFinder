using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VolcanoFinder.API.Models.DTOs;
using VolcanoFinder.API.Models.Entities;
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

        [HttpGet("{volcanoId}", Name = "GetVolcanoFromRegion")]
        public async Task<IActionResult> GetVolcanoFromRegion(int regionId, int volcanoId)
        {
            if (!await _volcanoFinderRepository.RegionExistsAsync(regionId))
                return NotFound();

            var volcanoEntity = await _volcanoFinderRepository.GetVolcanoFromRegionAsync(regionId, volcanoId);

            if (volcanoEntity is null)
                return NotFound();

            return Ok(_mapper.Map<VolcanoDto>(volcanoEntity));
        }

        [HttpPost]
        public async Task<IActionResult> AddVolcanoToRegion(int regionId, VolcanoForCreationDto volcanoForCreationDto)
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

        [HttpPut("volcanoId")]
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

        [HttpPatch("volcanoId")]
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
    }
}
