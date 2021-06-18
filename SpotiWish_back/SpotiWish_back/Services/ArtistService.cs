using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpotiWish_back.Model;
using SpotiWish_back.Repositories.Interface;
using SpotiWish_back.Services.Interface;

namespace SpotiWish_back.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _ArtistRepository;

        public ArtistService(IArtistRepository ArtistRepository)
        {
            _ArtistRepository = ArtistRepository;
        }

        public async Task<Artist> AddArtist(CRUDArtistDTO newArtist)
        {
            var modelDb = await _ArtistRepository.AddArtist(newArtist);
            return modelDb;
        }

        public async Task<int> DeleteArtist(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");
            
            if(! await _ArtistRepository.ExistById(id))
                throw new NullReferenceException("Artist doesn't exist");
      
            return await _ArtistRepository.DeleteArtist(id);
        }

        public async Task<List<Artist>> GetAllArtist()
        {
            return  await _ArtistRepository.GetAllArtist();
        }

        public async Task<Artist> GetSingleArtist(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");
            
            if(! await _ArtistRepository.ExistById(id))
                throw new NullReferenceException("Artist doesn't exist");
        
            return await _ArtistRepository.GetSingleArtist(id);
        }

        public async Task<Artist> UpdateArtist(int id, CRUDArtistDTO model)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");
            
            if(! await _ArtistRepository.ExistById(id))
                throw new NullReferenceException("User doesn't exist");

            var modelDb =await _ArtistRepository.UpdateArtist(id, model);
            return modelDb;
        }

        public async Task<bool> SetProfilThumbnailArtist(int id, byte[] thumbnail)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");
            
            return await _ArtistRepository.SetProfilThumbnailArtist(id, thumbnail);
        }
        public async Task<byte[]> GetProfilThumbnailArtist(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");
            
            var ImageDb = await _ArtistRepository.GetProfilThumbnailArtist(id);
            return ImageDb;
        }
        public async Task<bool> SetBackThumbnailArtist(int id, byte[] thumbnail)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");
            
            return await _ArtistRepository.SetBackThumbnailArtist(id, thumbnail);
        }
        public async Task<byte[]> GetBackThumbnailArtist(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");
            
            var ImageDb = await _ArtistRepository.GetBackThumbnailArtist(id);
            return ImageDb;
        }
    }
}