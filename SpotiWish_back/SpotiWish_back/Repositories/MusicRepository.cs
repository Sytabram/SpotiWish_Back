using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpotiWish_back.Data;
using SpotiWish_back.Model;
using SpotiWish_back.Repositories.Interface;

namespace SpotiWish_back.Repositories
{
    public class MusicRepository : IMusicRepository
    {
        private readonly SpotiWishDataContext _context;

        public MusicRepository(SpotiWishDataContext context)
        {
            _context = context;
        }
        public async Task<Music> AddMusic(CRUDMusicDTO newMusic)
        {
            var model = new Music();
            model.Name = newMusic.Name;
            model.TimeOfPlays = newMusic.TimeOfPlays;
            model.Style = newMusic.Style;
            model.ReleaseDate = newMusic.ReleaseDate;
            _context.Musics.Add(model);
            
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<int> DeleteMusic(int id)
        {
            _context.Musics.Remove(await _context.Musics.FindAsync(id));
            await _context.SaveChangesAsync();
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Music>> GetAllMusic()
        {
            return await _context.Musics
                .ToListAsync();
        }

        public async Task<Music> GetSingleMusic(int id)
        {
            var Music = await _context.Musics
                .FirstOrDefaultAsync(u=> u.Id == id) ;
            return Music;
        }

        public async Task<Music> UpdateMusic(int id, CRUDMusicDTO MusicToEdit)
        {
            var model = await _context.Musics.FindAsync(id);
            model.Name = MusicToEdit.Name;
            model.TimeOfPlays = MusicToEdit.TimeOfPlays;
            model.Style = MusicToEdit.Style;
            model.ReleaseDate = MusicToEdit.ReleaseDate;

            await _context.SaveChangesAsync();
            return await GetSingleMusic(id);
        }

        public async Task<bool> SetThumbnailMusic(int id, byte[] thumbnail)
        {
            var SpotiDB = _context.Musics.Find(id);
            SpotiDB.Thumbnail = thumbnail;
            return (await _context.SaveChangesAsync()) == 1;
        }

        public async Task<byte[]> GetThumbnailMusic(int id)
        {
            return (await _context.Musics.FindAsync(id)).Thumbnail;
        }
        public async Task<bool> SetSongMusic(int id, byte[] song)
        {
            var SpotiDB = _context.Musics.Find(id);
            SpotiDB.Thumbnail = song;
            return (await _context.SaveChangesAsync()) == 1;
        }

        public async Task<byte[]> GetSongMusic(int id)
        {
            return (await _context.Musics.FindAsync(id)).Thumbnail;
        }
        public Task<bool> ExistById(int id)
        {
            return _context.Musics.AnyAsync(u => u.Id == id);
        }
    }
}