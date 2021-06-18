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
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        private readonly IMapper _mapper;
        private readonly ILogger<AlbumController> _logger;
        public AlbumController(IAlbumService albumService, IMapper mapper, ILogger<AlbumController> logger)
        {
            _albumService = albumService;
            _mapper = mapper;
            _logger = logger;
        }
        [Authorize(Roles = "user, admin")]
        [HttpGet("Album")]
        public async Task<IActionResult> GetAllAlbum()
        {   
            _logger.LogInformation("Get Album");
            var Albums = await _albumService.GetAllAlbum();
            return Ok(_mapper.Map<List<Album>, List<AlbumDTO>>(Albums));
        }
        [Authorize(Roles = "admin")]
        [HttpPost("Album/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CRUDAlbumDTO AlbumToEdit)
        {
            _logger.LogInformation("Post Album/{id}");
            var modelDb = await _albumService.UpdateAlbum(id, AlbumToEdit);

            var modelDto = _mapper.Map<AlbumDTO>(modelDb);
            return Ok(modelDto);
        }
        [Authorize(Roles = "user, admin")]
        [HttpGet("Album/{id}")]
        public async Task<IActionResult> GetSingleAlbum([FromRoute] int id)
        {   
            _logger.LogInformation("Get Album/{id}");
            var Album = await _albumService.GetSingleAlbum(id);

            if (Album == null)
                return NotFound();
            
            return Ok(_mapper.Map<AlbumDTO>(Album));
        }
        [Authorize(Roles = "admin")]
        [HttpPost("Album")]
        public async Task<IActionResult> CreatAlbum([FromBody] CRUDAlbumDTO AlbumToCreate)
        {
            _logger.LogInformation("Post Album");
            var modelDB = await _albumService.AddAlbum(AlbumToCreate);
            var modelDTO = _mapper.Map<AlbumDTO>(modelDB);
            
            return Created($"Albums/{modelDTO.Id}", modelDTO);
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("Album/{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            _logger.LogInformation("Delete Album/{id}");
            await _albumService.DeleteAlbum(id);
            return NoContent();
        }
        [Authorize(Roles = "admin")]
        [HttpPost("Album/{id}/thumbnail")]
        public async Task<IActionResult> SetThumbnailAlbum([FromRoute] int id, IFormFile file)
        {
            _logger.LogInformation("Post Album/{id}/thumbnail");
            var ms = new MemoryStream();
            file.CopyTo(ms);
            await _albumService.SetThumbnailAlbum(id, ms.ToArray());
            return Ok();
        }
        [HttpGet("Album/{id}/thumbnail")]
        public async Task<IActionResult> GetThumbnailAlbum([FromRoute] int id)
        {
            _logger.LogInformation("Get Album/{id}/thumbnail");
            return File(await _albumService.GetThumbnailAlbum(id), "image/jpeg");
        }
    }
}