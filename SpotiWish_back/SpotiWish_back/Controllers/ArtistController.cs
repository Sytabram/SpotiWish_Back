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
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;
        private readonly ILogger<ArtistController> _logger;
        public ArtistController(IArtistService artistService, IMapper mapper, ILogger<ArtistController> logger)
        {
            _artistService = artistService;
            _mapper = mapper;
            _logger = logger;
        }
        [Authorize(Roles = "user, admin")]
        [HttpGet("Artist")]
        public async Task<IActionResult> GetAllArtist()
        {   
            _logger.LogInformation("Get Artist");
            var Artists = await _artistService.GetAllArtist();
            return Ok(_mapper.Map<List<Artist>, List<ArtistDTO>>(Artists));
        }
        [Authorize(Roles = "admin")]
        [HttpPost("Artist/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CRUDArtistDTO ArtistToEdit)
        {
            _logger.LogInformation("Post Artist/{id}");
            var modelDb = await _artistService.UpdateArtist(id, ArtistToEdit);

            var modelDto = _mapper.Map<ArtistDTO>(modelDb);
            return Ok(modelDto);
        }
        [Authorize(Roles = "user, admin")]
        [HttpGet("Artist/{id}")]
        public async Task<IActionResult> GetSingleArtist([FromRoute] int id)
        {   
            _logger.LogInformation("Get Artist/{id}");
            var Artist = await _artistService.GetSingleArtist(id);

            if (Artist == null)
                return NotFound();
            
            return Ok(_mapper.Map<ArtistDTO>(Artist));
        }
        [Authorize(Roles = "admin")]
        [HttpPost("Artist")]
        public async Task<IActionResult> CreatArtist([FromBody] CRUDArtistDTO ArtistTocreat)
        {
            _logger.LogInformation("Post Artist");
            var modelDB = await _artistService.AddArtist(ArtistTocreat);
            var modelDTO = _mapper.Map<ArtistDTO>(modelDB);
            
            return Created($"Artists/{modelDTO.Id}", modelDTO);
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("Artist/{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            _logger.LogInformation("Delete Artist/{id}");
            await _artistService.DeleteArtist(id);
            return NoContent();
        }
        [Authorize(Roles = "admin")]
        [HttpPost("Artist/{id}/profilThumbnail")]
        public async Task<IActionResult> SetProfilThumbnailArtist([FromRoute] int id, IFormFile file)
        {
            _logger.LogInformation("Post Artist/{id}/profilThumbnail");
            var ms = new MemoryStream();
            file.CopyTo(ms);
            await _artistService.SetProfilThumbnailArtist(id, ms.ToArray());
            return Ok();
        }
        [HttpGet("Artist/{id}/profilThumbnail")]
        public async Task<IActionResult> GetProfilThumbnailArtist([FromRoute] int id)
        {
            _logger.LogInformation("Get Artist/{id}/profilThumbnail");
            return File(await _artistService.GetProfilThumbnailArtist(id), "image/jpeg");
        }
        [Authorize(Roles = "admin")]
        [HttpPost("Artist/{id}/backThumbnail")]
        public async Task<IActionResult> SetBackThumbnailArtist([FromRoute] int id, IFormFile file)
        {
            _logger.LogInformation("Post Artist/{id}/backThumbnail");
            var ms = new MemoryStream();
            file.CopyTo(ms);
            await _artistService.SetBackThumbnailArtist(id, ms.ToArray());
            return Ok();
        }
        [HttpGet("Artist/{id}/backThumbnail")]
        public async Task<IActionResult> GetBackThumbnailArtist([FromRoute] int id)
        {
            _logger.LogInformation("Get Artist/{id}/backThumbnail");
            return File(await _artistService.GetBackThumbnailArtist(id), "image/jpeg");
        }
        //todo ajouter supprimer une musique
    }
}