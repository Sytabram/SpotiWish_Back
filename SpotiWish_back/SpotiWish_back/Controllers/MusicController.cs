using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpotiWish_back.Controllers.Exception;
using SpotiWish_back.Model;
using SpotiWish_back.Services.Interface;

namespace SpotiWish_back.Controllers
{
    [ApiController]
    public class musicController : ControllerBase
    {
        private readonly IMusicService _musicService;
        private readonly IMapper _mapper;
        private readonly ILogger<musicController> _logger;
        public musicController(IMusicService musicService, IMapper mapper, ILogger<musicController> logger)
        {
            _musicService = musicService;
            _mapper = mapper;
            _logger = logger;
        }
        [Authorize(Roles = "user, admin")]
        [HttpGet("music")]
        public async Task<IActionResult> GetAllMusic()
        {   
            _logger.LogInformation("Get music");
            var Musics = await _musicService.GetAllMusic();
            return Ok(_mapper.Map<List<Music>, List<MusicDTO>>(Musics));
        }
        [HttpGet("10music")]
        public async Task<IActionResult> Get10Music()
        {   
            _logger.LogInformation("Get 10music");
            var Musics = await _musicService.Get10Music();
            return Ok(_mapper.Map<List<Music>, List<MusicDTO>>(Musics));
        }
        [Authorize(Roles = "admin")]
        [HttpPost("Music/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CRUDMusicDTO MusicToEdit)
        {
            _logger.LogInformation("Post Music/{id}");
            var modelDb = await _musicService.UpdateMusic(id, MusicToEdit);

            var modelDto = _mapper.Map<MusicDTO>(modelDb);
            return Ok(modelDto);
        }
        [Authorize(Roles = "user, admin")]
        [HttpGet("Music/{id}")]
        public async Task<IActionResult> GetSingleMusic([FromRoute] int id)
        {   
            _logger.LogInformation("Get Music/{id}");
            var Music = await _musicService.GetSingleMusic(id);

            if (Music == null)
                return NotFound();
            
            return Ok(_mapper.Map<MusicDTO>(Music));
        }
        [Authorize(Roles = "admin")]
        [HttpPost("Music")]
        public async Task<IActionResult> CreatMusic([FromBody] CRUDMusicDTO MusicTocreat)
        {
            _logger.LogInformation("Post Music");
            var modelDB = await _musicService.AddMusic(MusicTocreat);
            var modelDTO = _mapper.Map<MusicDTO>(modelDB);
            
            return Created($"Musics/{modelDTO.Id}", modelDTO);
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("Music/{id}")]
        public async Task<IActionResult> DeleteMusic(int id)
        {
            await _musicService.DeleteMusic(id);
            return NoContent();
        }
        [Authorize(Roles = "admin")]
        [HttpPost("Music/{id}/thumbnail")]
        public async Task<IActionResult> SetThumbnailMusic([FromRoute] int id, IFormFile file)
        {
            _logger.LogInformation("Post Music/{id}/thumbnail");
            var ms = new MemoryStream();
            file.CopyTo(ms);
            await _musicService.SetThumbnailMusic(id, ms.ToArray());
            return Ok();
        }
        [HttpGet("Music/{id}/thumbnail")]
        public async Task<IActionResult> GetThumbnailMusic([FromRoute] int id)
        {
            _logger.LogInformation("Get Music/{id}/thumbnail");
            return File(await _musicService.GetThumbnailMusic(id), "image/jpeg");
        }
        [Authorize(Roles = "admin")]
        [HttpPost("Music/{id}/song")]
        public async Task<IActionResult> SetSongMusic([FromRoute] int id, IFormFile file)
        {
            _logger.LogInformation("Post Music/{id}/song");
            var ms = new MemoryStream();
            file.CopyTo(ms);
            await _musicService.SetSongMusic(id, ms.ToArray());
            return Ok();
        }
        [HttpGet("Music/{id}/song")]
        public async Task<IActionResult> GetSongMusic([FromRoute] int id)
        {
            _logger.LogInformation("Get Music/{id}/song");
            return File(await _musicService.GetSongMusic(id), "audio/mpeg");
        }
    }
}