using System.Collections.Generic;
using System.Threading.Tasks;
using SpotiWish_back.Model;

namespace SpotiWish_back.Services.Interface
{
    public interface IAlbumService
    {
        Task<Album> AddAlbum(CRUDAlbumDTO newAlbum);
        Task<int> DeleteAlbum(int id);
        Task<List<Album>> GetAllAlbum();
        Task<Album> GetSingleAlbum(int id);
        Task<Album> UpdateAlbum(int id, CRUDAlbumDTO model);
        Task<bool> SetThumbnailAlbum(int id, byte[] thumbnail);
        Task<byte[]> GetThumbnailAlbum(int id);
    }
}