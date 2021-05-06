using System.Collections.Generic;
using System.Threading.Tasks;
using SpotiWish_back.Model;

namespace SpotiWish_back.Repositories.Interface
{
    public interface IPlayListRepository
    {
        Task<PlayList> AddPlaylist(CRUDPlayListDTO newPLayList);
        Task<int> DeletePlaylist(int id);
        Task<List<PlayList>> GetAllPlayList();
        Task<PlayList> GetSinglePlayList(int id);
        Task<PlayList> UpdatePlayList(int id, CRUDPlayListDTO model);
        Task<bool> SetThumbnailPlayList(int id, byte[] thumbnail);
        Task<byte[]> GetThumbnailPlayList(int id);
        Task<bool> ExistById(int id);
    }
}