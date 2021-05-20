using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpotiWish_back.Model;
using SpotiWish_back.Repositories.Interface;
using SpotiWish_back.Services.Interface;

namespace SpotiWish_back.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _AlbumRepository;

        public AlbumService(IAlbumRepository AlbumRepository)
        {
            _AlbumRepository = AlbumRepository;
        }

        public async Task<Album> AddAlbum(CRUDAlbumDTO newAlbum)
        {
            var modelDb = await _AlbumRepository.AddAlbum(newAlbum);
            return modelDb;
        }

        public async Task<int> DeleteAlbum(int id)
        {
            if(! await _AlbumRepository.ExistById(id))
                throw new NullReferenceException("Album doesn't exist");
      
            return await _AlbumRepository.DeleteAlbum(id);
        }

        public async Task<List<Album>> GetAllAlbum()
        {
            return  await _AlbumRepository.GetAllAlbum();
        }

        public async Task<Album> GetSingleAlbum(int id)
        {
            if(! await _AlbumRepository.ExistById(id))
                throw new NullReferenceException("Album doesn't exist");
        
            return await _AlbumRepository.GetSingleAlbum(id);
        }

        public async Task<Album> UpdateAlbum(int id, CRUDAlbumDTO model)
        {
           
            if(! await _AlbumRepository.ExistById(id))
                throw new NullReferenceException("User doesn't exist");

            var modelDb =await _AlbumRepository.UpdateAlbum(id, model);
            return modelDb;
        }

        public async Task<bool> SetThumbnailAlbum(int id, byte[] thumbnail)
        {
            return await _AlbumRepository.SetThumbnailAlbum(id, thumbnail);
        }
        public async Task<byte[]> GetThumbnailAlbum(int id)
        {
            var ImageDb = await _AlbumRepository.GetThumbnailAlbum(id);
            return ImageDb;
        }
    }
}