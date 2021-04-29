using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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

        [HttpGet("PlayList")]
        public async Task<IActionResult> GetAllPlayList()
        {   
            var playLists = await _playListService.GetAllPlayList();
            return Ok(_mapper.Map<List<PlayList>, List<PlayListDTO>>(playLists));
        }
        //     - GetPlaylistLikedSong à partir de l'user (titre, album, date de sortie, durée)
        //     - GetAll Playlist à partir de l'user (nom de la playlist, vignette, nombre de musique, liste des musiques dedans)
        // crud
    }
}