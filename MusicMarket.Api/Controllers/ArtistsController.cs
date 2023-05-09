using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicMarket.Api.DTO;
using MusicMarket.Api.Validators;
using MusicMarket.Core.Entities;
using MusicMarket.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;

        public ArtistsController(IArtistService artistService, IMapper mapper)
        {
            _artistService = artistService;
            _mapper = mapper;
        }
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ArtistDto>>> GetAllArtists()
        {
            var artists=await _artistService.GetAllArtists();
            var artistResource=_mapper.Map<IEnumerable<ArtistDto>>(artists);
            return Ok(artistResource);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDto>> GetArtistById(int id)
        {
            var artists=await _artistService.GetArtistById(id);
            var artistResource = _mapper.Map<ArtistDto>(artists);
            return Ok(artistResource);
        }
        [HttpPost]
        public async Task<ActionResult<ArtistDto>> CreateArtist([FromBody] SaveArtistDto saveArtistResource)
        {
            var validator = new SaveArtistResourceValidator();
            var validationResult = await validator.ValidateAsync(saveArtistResource);
            if(!validationResult.IsValid)
              return BadRequest(validationResult.Errors);

            var artistToCreate = _mapper.Map<Artist>(saveArtistResource);
            var newArtist=await _artistService.CreateArtist(artistToCreate);
            var artist=await _artistService.GetArtistById(newArtist.Id);

            var artistResource = _mapper.Map<ArtistDto>(artist);

            return Ok(artistResource);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ArtistDto>> UpdateArtist(int id, [FromBody] SaveArtistDto saveArtistResource)
        {
            var validator=new SaveArtistResourceValidator();
            var validationResult=await validator.ValidateAsync(saveArtistResource);
            if(!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var artistToBeUpdated = await _artistService.GetArtistById(id);
            if (artistToBeUpdated == null)
                return NotFound();
            var artist=_mapper.Map<Artist>(saveArtistResource);
            await _artistService.UpdateArtist(artistToBeUpdated, artist);

            var updateArtist = await _artistService.GetArtistById(id);
            var updateArtistResource=_mapper.Map<ArtistDto>(updateArtist);

            return Ok(updateArtistResource);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var artist=await _artistService.GetArtistById(id);
            await _artistService.DeleteArtist(artist);
            return NoContent();
        }
    }
}
