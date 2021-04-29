using System.Collections.Generic;
using System.Threading.Tasks;
using SpotiWish_back.Model;
using SpotiWish_back.Repositories.Interface;
using SpotiWish_back.Services.Interface;

namespace SpotiWish_back.Services
{
    public class PlayListService : IPlayListService
    {
        private readonly IPlayListRepository _playListRepository;

        public PlayListService(IPlayListRepository playListRepository)
        {
            _playListRepository = playListRepository;
        }
        public async Task<List<PlayList>> GetAllPlayList()
        {
            return  await _playListRepository.GetAllPlayList();
        }
    }
}