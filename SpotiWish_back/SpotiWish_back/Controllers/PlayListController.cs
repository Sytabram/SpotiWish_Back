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
    public class PlayListController : ControllerBase
    {
        private readonly IPlayListService _playListService;
        private readonly IMapper _mapper;
        private readonly ILogger<PlayListController> _logger;
        public PlayListController(IPlayListService playListService, IMapper mapper, ILogger<PlayListController> logger)
        {
            _playListService = playListService;
            _mapper = mapper;
            _logger = logger;
        }
        //return empty idk why
        [Authorize(Roles = "user, admin")]
        [HttpGet("playlist")]
        public async Task<IActionResult> GetAllPlayList()
        {   
            _logger.LogInformation("Get playlist");
            var playLists = await _playListService.GetAllPlayList();
            return Ok(_mapper.Map<List<PlayList>, List<PlayListDTO>>(playLists));
        }
        [Authorize(Roles = "user, admin")]
        [HttpPost("playlist/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CRUDPlayListDTO playListToEdit)
        {
            _logger.LogInformation("Post playlist/{id}");
            var modelDb = await _playListService.UpdatePlayList(id, playListToEdit);

            var modelDto = _mapper.Map<PlayListDTO>(modelDb);
            return Ok(modelDto);
        }
        [Authorize(Roles = "user, admin")]
        [HttpGet("playlist/{id}")]
        public async Task<IActionResult> GetSinglePlayList([FromRoute] int id)
        {   
            _logger.LogInformation("Get playlist/{id}");
            var playlist = await _playListService.GetSinglePlayList(id);

            if (playlist == null)
                return NotFound();
            
            return Ok(_mapper.Map<PlayListDTO>(playlist));
        }
        [Authorize(Roles = "user, admin")]
        [HttpPost("playlist")]
        public async Task<IActionResult> CreatPlayList([FromBody] CRUDPlayListDTO playListTocreat)
        {
            _logger.LogInformation("Post playlist");
            var modelDB = await _playListService.AddPlaylist(playListTocreat);
            var modelDTO = _mapper.Map<PlayListDTO>(modelDB);
            
            return Created($"playlists/{modelDTO.Id}", modelDTO);
        }
        [Authorize(Roles = "user, admin")]
        [HttpDelete("playlist/{id}")]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            _logger.LogInformation("Delete playlist/{id}");
            await _playListService.DeletePlaylist(id);
            return NoContent();
        }
        [Authorize(Roles = "user, admin")]
        [HttpPost("playlist/{id}/thumbnail")]
        public async Task<IActionResult> SetThumbnailPlayList([FromRoute] int id, IFormFile file)
        {
            _logger.LogInformation("Post playlist/{id}/thumbnail");
            var ms = new MemoryStream();
            file.CopyTo(ms);
            await _playListService.SetThumbnailPlayList(id, ms.ToArray());
            return Ok();
        }
        [HttpGet("playlist/{id}/thumbnail")]
        public async Task<IActionResult> GetThumbnailPlayList([FromRoute] int id)
        {
            _logger.LogInformation("Get playlist/{id}/thumbnail");
            return File(await _playListService.GetThumbnailPlayList(id), "image/jpeg");
        }
    }
}