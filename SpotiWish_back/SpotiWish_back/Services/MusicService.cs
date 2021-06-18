using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpotiWish_back.Model;
using SpotiWish_back.Repositories.Interface;
using SpotiWish_back.Services.Interface;

namespace SpotiWish_back.Services
{
    public class MusicService : IMusicService
    {
        private readonly IMusicRepository _MusicRepository;

        public MusicService(IMusicRepository MusicRepository)
        {
            _MusicRepository = MusicRepository;
        }

        public async Task<Music> AddMusic(CRUDMusicDTO newMusic)
        {
            var modelDb = await _MusicRepository.AddMusic(newMusic);
            return modelDb;
        }

        public async Task<int> DeleteMusic(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");
            
            if(! await _MusicRepository.ExistById(id))
                throw new NullReferenceException("Music doesn't exist");
      
            return await _MusicRepository.DeleteMusic(id);
        }

        public async Task<List<Music>> GetAllMusic()
        {
            return  await _MusicRepository.GetAllMusic();
        }
        public async Task<List<Music>> Get10Music()
        {
            return  await _MusicRepository.Get10Music();
        }
        public async Task<Music> GetSingleMusic(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");
            
            if(! await _MusicRepository.ExistById(id))
                throw new NullReferenceException("Music doesn't exist");
        
            return await _MusicRepository.GetSingleMusic(id);
        }

        public async Task<Music> UpdateMusic(int id, CRUDMusicDTO model)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");
            
            if(! await _MusicRepository.ExistById(id))
                throw new NullReferenceException("User doesn't exist");

            var modelDb =await _MusicRepository.UpdateMusic(id, model);
            return modelDb;
        }

        public async Task<bool> SetThumbnailMusic(int id, byte[] thumbnail)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");
            
            return await _MusicRepository.SetThumbnailMusic(id, thumbnail);
        }
        public async Task<byte[]> GetThumbnailMusic(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");
            
            var ImageDb = await _MusicRepository.GetThumbnailMusic(id);
            return ImageDb;
        }
        public async Task<bool> SetSongMusic(int id, byte[] song)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");
            
            return await _MusicRepository.SetSongMusic(id, song);
        }
        public async Task<byte[]> GetSongMusic(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");
            
            var MusicDb = await _MusicRepository.GetSongMusic(id);
            return MusicDb;
        }
    }
}