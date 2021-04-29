using System.Collections.Generic;
using System.Threading.Tasks;
using SpotiWish_back.Model;

namespace SpotiWish_back.Services.Interface
{
    public class IPlayListService
    {
        public Task<PlayList> AddCategory(PlayListDTO newPLayList);
        public Task<int> DeleteCategory(int id);
        public Task<List<PlayList>> GetAllPlayList();
        public Task<PlayList> GetSinglePlayList(int id);
        public Task<PlayListDTO> UpdatePlayList(int id, PlayListDTO model);
        public Task<bool> SetThumbnailPlayList(int id, byte[] thumbnail);
    }
}