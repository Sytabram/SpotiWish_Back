using System.Collections.Generic;
using System.Threading.Tasks;
using SpotiWish_back.Model;

namespace SpotiWish_back.Services.Interface
{
    public interface IArtistService
    {
        Task<Artist> AddArtist(CRUDArtistDTO newArtist);
        Task<int> DeleteArtist(int id);
        Task<List<Artist>> GetAllArtist();
        Task<Artist> GetSingleArtist(int id);
        Task<Artist> UpdateArtist(int id, CRUDArtistDTO model);
        Task<bool> SetProfilThumbnailArtist(int id, byte[] thumbnail);
        Task<byte[]> GetProfilThumbnailArtist(int id);
        Task<bool> SetBackThumbnailArtist(int id, byte[] thumbnail);
        Task<byte[]> GetBackThumbnailArtist(int id);
    }
}