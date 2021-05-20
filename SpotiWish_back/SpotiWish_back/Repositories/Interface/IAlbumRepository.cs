using System.Collections.Generic;
using System.Threading.Tasks;
using SpotiWish_back.Model;

namespace SpotiWish_back.Repositories.Interface
{
    public interface IAlbumRepository
    {
        Task<Album> AddAlbum(CRUDAlbumDTO newAlbum);
        Task<int> DeleteAlbum(int id);
        Task<List<Album>> GetAllAlbum();
        Task<Album> GetSingleAlbum(int id);
        Task<Album> UpdateAlbum(int id, CRUDAlbumDTO model);
        Task<bool> SetThumbnailAlbum(int id, byte[] thumbnail);
        Task<byte[]> GetThumbnailAlbum(int id);
        Task<bool> ExistById(int id);
    }
}