using System.Collections.Generic;
using System.Threading.Tasks;
using SpotiWish_back.Model;
using SpotiWish_back.Repositories.Interface;

namespace SpotiWish_test.Services
{
    public class ArtistRepositoryMock : IArtistRepository
    {
        public Task<Artist> AddArtist(CRUDArtistDTO newArtist)
        {
            return null;
        }
        public Task<Artist> GetSingleArtist(int id)
        {
            return Task.FromResult(new Artist() 
            {
                Id = id,
                Name= "T1"
            });
        }

        public Task<int> DeleteArtist(int id)
        {
            return Task.FromResult(1);
        }

        public Task<bool> ExistById(int id)
        {
            return Task.FromResult(true);
        }

        public Task<byte[]> GetProfilThumbnailArtist(int id)
        {
            return null;
        }
        

        public Task<bool> SetProfilThumbnailArtist(int id, byte[] thumbnail)
        {
            return Task.FromResult(true);
        }

        public Task<byte[]> GetBackThumbnailArtist(int id)
        {
            return null;
        }
        

        public Task<bool> SetBackThumbnailArtist(int id, byte[] thumbnail)
        {
            return Task.FromResult(true);
        }
        public Task<List<Artist>> GetAllArtist()
        {
            return null;
        }

        public Task<Artist> UpdateArtist(int id, CRUDArtistDTO model)
        {
            return null;
        }
    }
}