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

        public Task<PlayList> AddCategory(PlayListDTO newPLayList)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteCategory(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<PlayList>> GetAllPlayList()
        {
            return  await _playListRepository.GetAllPlayList();
        }

        public Task<PlayList> GetSinglePlayList(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PlayListDTO> UpdatePlayList(int id, PlayListDTO model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SetThumbnailPlayList(int id, byte[] thumbnail)
        {
            throw new System.NotImplementedException();
        }
    }
}