using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ArtistController(IArtistService artistService, IMapper mapper)
        {
            _artistService = artistService;
            _mapper = mapper;
        }
        [Authorize(Roles = "user, admin")]
        [HttpGet("Artist")]
        public async Task<IActionResult> GetAllArtist()
        {   
            var Artists = await _artistService.GetAllArtist();
            return Ok(_mapper.Map<List<Artist>, List<ArtistDTO>>(Artists));
        }
        [Authorize(Roles = "admin")]
        [HttpPost("Artist/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CRUDArtistDTO ArtistToEdit)
        {
            var modelDb = await _artistService.UpdateArtist(id, ArtistToEdit);

            var modelDto = _mapper.Map<ArtistDTO>(modelDb);
            return Ok(modelDto);
        }
        [Authorize(Roles = "user, admin")]
        [HttpGet("Artist/{id}")]
        public async Task<IActionResult> GetSingleArtist([FromRoute] int id)
        {   
            var Artist = await _artistService.GetSingleArtist(id);

            if (Artist == null)
                return NotFound();
            
            return Ok(_mapper.Map<ArtistDTO>(Artist));
        }
        [Authorize(Roles = "admin")]
        [HttpPost("Artist")]
        public async Task<IActionResult> CreatArtist([FromBody] CRUDArtistDTO ArtistTocreat)
        {
            var modelDB = await _artistService.AddArtist(ArtistTocreat);
            var modelDTO = _mapper.Map<ArtistDTO>(modelDB);
            
            return Created($"Artists/{modelDTO.Id}", modelDTO);
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("Artist/{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            await _artistService.DeleteArtist(id);
            return NoContent();
        }
        [Authorize(Roles = "admin")]
        [HttpPost("Artist/{id}/profilThumbnail")]
        public async Task<IActionResult> SetProfilThumbnailArtist([FromRoute] int id, IFormFile file)
        {
            var ms = new MemoryStream();
            file.CopyTo(ms);
            await _artistService.SetProfilThumbnailArtist(id, ms.ToArray());
            return Ok();
        }
        [Authorize(Roles = "user, admin")]
        [HttpGet("Artist/{id}/profilThumbnail")]
        public async Task<IActionResult> GetProfilThumbnailArtist([FromRoute] int id)
        {
            return File(await _artistService.GetProfilThumbnailArtist(id), "image/jpeg");
        }
        [Authorize(Roles = "admin")]
        [HttpPost("Artist/{id}/backThumbnail")]
        public async Task<IActionResult> SetBackThumbnailArtist([FromRoute] int id, IFormFile file)
        {
            var ms = new MemoryStream();
            file.CopyTo(ms);
            await _artistService.SetBackThumbnailArtist(id, ms.ToArray());
            return Ok();
        }
        [Authorize(Roles = "user, admin")]
        [HttpGet("Artist/{id}/backThumbnail")]
        public async Task<IActionResult> GetBackThumbnailArtist([FromRoute] int id)
        {
            return File(await _artistService.GetBackThumbnailArtist(id), "image/jpeg");
        }
        //todo ajouter supprimer une musique
    }
}