using System.Collections.Generic;
using System.Threading.Tasks;
using SpotiWish_back.Model;

namespace SpotiWish_back.Services.Interface
{
    public interface IPlayListService   
    {
        Task<PlayList> AddCategory(PlayListDTO newPLayList);
        Task<int> DeleteCategory(int id);
        Task<List<PlayList>> GetAllPlayList();
        Task<PlayList> GetSinglePlayList(int id);
        Task<PlayListDTO> UpdatePlayList(int id, PlayListDTO model);
        Task<bool> SetThumbnailPlayList(int id, byte[] thumbnail);
    }
}