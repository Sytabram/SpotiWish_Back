using System.Collections.Generic;
using System.Threading.Tasks;
using SpotiWish_back.Model;
using SpotiWish_back.Repositories.Interface;

namespace SpotiWish_test.Services
{
    public class MusicRepositoryMock : IMusicRepository
    {
        public Task<Music> AddMusic(CRUDMusicDTO newMusic)
        {
            return null;
        }
        public Task<Music> GetSingleMusic(int id)
        {
            return Task.FromResult(new Music() 
            {
                Id = id,
                Name= "T1"
            });
        }

        public Task<int> DeleteMusic(int id)
        {
            return Task.FromResult(1);
        }

        public Task<bool> ExistById(int id)
        {
            return Task.FromResult(true);
        }

        public Task<byte[]> GetThumbnailMusic(int id)
        {
            return null;
        }
        
        public Task<bool> SetThumbnailMusic(int id, byte[] thumbnail)
        {
            return Task.FromResult(true);
        }
        
        public Task<byte[]> GetSongMusic(int id)
        {
            return null;
        }
        
        public Task<bool> SetSongMusic(int id, byte[] song)
        {
            return Task.FromResult(true);
        }

        public Task<List<Music>> GetAllMusic()
        {
            return null;
        }

        public Task<Music> UpdateMusic(int id, CRUDMusicDTO model)
        {
            return null;
        }
        public Task<List<Music>> Get10Music()
        {
            return null;
        }
    }
}