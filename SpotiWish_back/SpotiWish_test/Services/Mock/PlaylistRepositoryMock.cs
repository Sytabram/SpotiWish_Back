using System.Collections.Generic;
using System.Threading.Tasks;
using SpotiWish_back.Model;
using SpotiWish_back.Repositories.Interface;

namespace SpotiWish_test.Services
{
    public class PlaylistRepositoryMock : IPlayListRepository
    {
        public Task<PlayList> AddPlaylist(CRUDPlayListDTO newPLayList)
        {
            return null;
        }
        public Task<PlayList> GetSinglePlayList(int id)
        {
            return Task.FromResult(new PlayList() 
            {
                Id = id,
                Name= "T1"
            });
        }

        public Task<int> DeletePlaylist(int id)
        {
            return Task.FromResult(1);
        }

        public Task<bool> ExistById(int id)
        {
            return Task.FromResult(true);
        }

        public Task<byte[]> GetThumbnailPlayList(int id)
        {
            return null;
        }
        

        public Task<bool> SetThumbnailPlayList(int id, byte[] thumbnail)
        {
            return Task.FromResult(true);
        }

        public Task<List<PlayList>> GetAllPlayList()
        {
            return null;
        }

        public Task<PlayList> UpdatePlayList(int id, CRUDPlayListDTO playlistUpdate)
        {
            return null;
        }
    }
}