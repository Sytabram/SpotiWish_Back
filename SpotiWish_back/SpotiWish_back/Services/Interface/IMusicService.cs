using System.Collections.Generic;
using System.Threading.Tasks;
using SpotiWish_back.Model;

namespace SpotiWish_back.Services.Interface
{
    public interface IMusicService
    {
        Task<Music> AddMusic(CRUDMusicDTO newMusic);
        Task<int> DeleteMusic(int id);
        Task<List<Music>> GetAllMusic();
        Task<List<Music>> Get10Music();
        Task<Music> GetSingleMusic(int id);
        Task<Music> UpdateMusic(int id, CRUDMusicDTO model);
        Task<bool> SetThumbnailMusic(int id, byte[] thumbnail);
        Task<byte[]> GetThumbnailMusic(int id);
        Task<bool> SetSongMusic(int id, byte[] song);
        Task<byte[]> GetSongMusic(int id);
    }
}