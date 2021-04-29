using System.Collections.Generic;
using System.Threading.Tasks;
using SpotiWish_back.Model;
using SpotiWish_back.Repositories.Interface;

namespace SpotiWish_back.Repositories
{
    public class PlayListRepository : IPlayListRepository
    {
        public Task<PlayList> AddCategory(PlayListDTO newPLayList)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteCategory(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<PlayList>> GetAllPlayList()
        {
            throw new System.NotImplementedException();
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