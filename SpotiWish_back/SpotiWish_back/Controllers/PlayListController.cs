using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotiWish_back.Controllers.Exception;
using SpotiWish_back.Services.Interface;

namespace SpotiWish_back.Controllers
{
    [ApiController]
    public class PlayListController : ControllerBase
    {
        private readonly IPlayListService _playListService;

        public PlayListController(IPlayListService playListService)
        {
            _playListService = playListService;
        }

        [HttpGet]
        public async Task<IPlayListService> GetAllPlayList()
        {
            try
            {
                return Ok(await _playListService.GetAllPlayList());
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
                throw;
            }
        }
        //     - GetPlaylistLikedSong à partir de l'user (titre, album, date de sortie, durée)
        //     - GetAll Playlist à partir de l'user (nom de la playlist, vignette, nombre de musique, liste des musiques dedans)
        // crud
    }
}