using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicMarket.Api.DTO;
using MusicMarket.Api.Validators;
using MusicMarket.Core.Entities;
using MusicMarket.Services.Services;

namespace MusicMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicsController : ControllerBase
    {
        private readonly IMusicService _musicService;
        private readonly IMapper _mapper;

        public MusicsController(IMusicService musicService, IMapper mapper)
        {
            _musicService = musicService;
            _mapper = mapper;
        }
        [HttpGet("")]

        public async Task<ActionResult<IEnumerable<MusicDto>>> GetAllMusics()
        {
            var musics = await _musicService.GetAllWithArtist();
            var musicResources = _mapper.Map<IEnumerable<MusicDto>>(musics);
            return Ok(musicResources);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MusicDto>> GetMusicById(int id)
        {
            var music=await _musicService.GetMusicById(id);
            //var muzisyen = music.Artist.Name;
            var musicResource=_mapper.Map<MusicDto>(music);
            return Ok(musicResource);
        }
        [HttpPost("")]
        public async Task<ActionResult<MusicDto>> CreateMusic([FromBody] SaveMusicDto saveMusicResource)
        {
            var validator = new SaveMusicResourceValidator();
            var validationResult=await validator.ValidateAsync(saveMusicResource);
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var newMusic = await _musicService.CreateMusic(_mapper.Map<Music>(saveMusicResource));

            return Ok(_mapper.Map<MusicDto>(newMusic));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<MusicDto>> UpdateMusic(int id, [FromBody] SaveMusicDto saveMusicResource)
        {
            var validator = new SaveMusicResourceValidator();
            var validationResult=await validator.ValidateAsync(saveMusicResource);
            var requestIsInvalid=id==0|| !validationResult.IsValid;
            if(requestIsInvalid)
            {
                return BadRequest(validationResult.Errors);
            }
            var musicToBeUpdate=await _musicService.GetMusicById(id);
            if(musicToBeUpdate==null)
            {
                return NotFound();
            }
            var music=_mapper.Map<Music>(saveMusicResource);
            await _musicService.UpdateMusic(musicToBeUpdate, music);
            var updatedMusic = await _musicService.GetMusicById(id);
            var updateMusicResource=_mapper.Map<MusicDto>(updatedMusic);
            return Ok(updateMusicResource);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusic(int id)
        {
            if(id==0)
            {
                return BadRequest();
            }
            var music=await _musicService.GetMusicById(id);
            if(music==null)
            {
                return NotFound();
            }
            await _musicService.DeleteMusic(music);
            return NoContent();
        }
    }
}
