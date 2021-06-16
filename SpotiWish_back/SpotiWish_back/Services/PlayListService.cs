using System;
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

        public async Task<PlayList> AddPlaylist(CRUDPlayListDTO newPLayList)
        {
            var modelDb = await _playListRepository.AddPlaylist(newPLayList);
            return modelDb;
        }

        public async Task<int> DeletePlaylist(int id)
        {
            if(! await _playListRepository.ExistById(id))
                throw new NullReferenceException("Playlist doesn't exist");
            
            return await _playListRepository.DeletePlaylist(id);
        }

        public async Task<List<PlayList>> GetAllPlayList()
        {
            return  await _playListRepository.GetAllPlayList();
        }

        public async Task<PlayList> GetSinglePlayList(int id)
        {
            if(! await _playListRepository.ExistById(id))
                throw new NullReferenceException("Playlist doesn't exist");
        
            return await _playListRepository.GetSinglePlayList(id);
        }

        public async Task<PlayList> UpdatePlayList(int id, CRUDPlayListDTO model)
        {
           
            if(! await _playListRepository.ExistById(id))
                throw new NullReferenceException("User doesn't exist");

            var modelDb =await _playListRepository.UpdatePlayList(id, model);
            return modelDb;
        }

        public async Task<bool> SetThumbnailPlayList(int id, byte[] thumbnail)
        {
            return await _playListRepository.SetThumbnailPlayList(id, thumbnail);
        }
        public async Task<byte[]> GetThumbnailPlayList(int id)
        {
            var ImageDb = await _playListRepository.GetThumbnailPlayList(id);
            return ImageDb;
        }
    }
}