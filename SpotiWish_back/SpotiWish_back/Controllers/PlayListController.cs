using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public PlayListController(IPlayListService playListService, IMapper mapper)
        {
            _playListService = playListService;
            _mapper = mapper;
        }
        //return empty idk why
        [HttpGet("playlist")]
        public async Task<IActionResult> GetAllPlayList()
        {   
            var playLists = await _playListService.GetAllPlayList();
            return Ok(_mapper.Map<List<PlayList>, List<PlayListDTO>>(playLists));
        }
        [HttpPost("playlist/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CRUDPlayListDTO playListToEdit)
        {
            var modelDb = await _playListService.UpdatePlayList(id, playListToEdit);

            var modelDto = _mapper.Map<PlayListDTO>(modelDb);
            return Ok(modelDto);
        }
        [HttpGet("playlist/{id}")]
        public async Task<IActionResult> GetSinglePlayList([FromRoute] int id)
        {   
            var playlist = await _playListService.GetSinglePlayList(id);

            if (playlist == null)
                return NotFound();
            
            return Ok(_mapper.Map<PlayListDTO>(playlist));
        }
        
        [HttpPost("playlist")]
        public async Task<IActionResult> CreatPlayList([FromBody] CRUDPlayListDTO playListTocreat)
        {
            var modelDB = await _playListService.AddPlaylist(playListTocreat);
            var modelDTO = _mapper.Map<PlayListDTO>(modelDB);
            
            return Created($"playlists/{modelDTO.Id}", modelDTO);
        }
        [HttpDelete("playlist/{id}")]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            await _playListService.DeletePlaylist(id);
            return NoContent();
        }

        [HttpPost("playlist/{id}/thumbnail")]
        public async Task<IActionResult> SetThumbnailPlayList([FromRoute] int id, IFormFile file)
        {
            var ms = new MemoryStream();
            file.CopyTo(ms);
            await _playListService.SetThumbnailPlayList(id, ms.ToArray());
            return Ok();
        }
        [HttpGet("playlist/{id}/thumbnail")]
        public async Task<IActionResult> GetThumbnailPlayList([FromRoute] int id)
        {
            return File(await _playListService.GetThumbnailPlayList(id), "image/jpeg");
        }
        //todo ajouter supprimer une musique
    }
}