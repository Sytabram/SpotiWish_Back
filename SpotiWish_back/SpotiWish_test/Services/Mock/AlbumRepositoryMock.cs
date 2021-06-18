using System.Collections.Generic;
using System.Threading.Tasks;
using SpotiWish_back.Model;
using SpotiWish_back.Repositories.Interface;

namespace SpotiWish_test.Services
{
    public class AlbumRepositoryMock : IAlbumRepository
    {
        public Task<Album> AddAlbum(CRUDAlbumDTO newAlbum)
        {
            return null;
        }
        public Task<Album> GetSingleAlbum(int id)
        {
            return Task.FromResult(new Album() 
            {
                Id = id,
                Name= "T1"
            });
        }

        public Task<int> DeleteAlbum(int id)
        {
            return Task.FromResult(1);
        }

        public Task<bool> ExistById(int id)
        {
            return Task.FromResult(true);
        }

        public Task<byte[]> GetThumbnailAlbum(int id)
        {
            return null;
        }
        

        public Task<bool> SetThumbnailAlbum(int id, byte[] thumbnail)
        {
            return Task.FromResult(true);
        }

        public Task<List<Album>> GetAllAlbum()
        {
            return null;
        }

        public Task<Album> UpdateAlbum(int id, CRUDAlbumDTO model)
        {
            return null;
        }
    }
}