using System;
using System.Collections.Generic;
using System.Linq;
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
            model.ReleaseDate = DateTime.Now;
            model.Albums = await GetAlbumById(newMusic.AlbumId);
            model.Playlists = await GetPlaylistById(newMusic.PlaylistId);
            _context.Musics.Add(model);
            
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<List<PlayList>> GetPlaylistById(List<int> idList)
        {
            return await _context.PlayLists
                .Include(x=>x.Musics)
                .Include(x=>x.Users)
                .Where(t => idList.Contains(t.Id)).ToListAsync();
                
        }
        public async Task<List<Album>> GetAlbumById(List<int> idList)
        {
            return await _context.Albums
                .Include(x=>x.Artists)
                .Include(x=>x.Musics)
                .Where(t => idList.Contains(t.Id)).ToListAsync();
            
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
                .Include(x=>x.Playlists)
                .Include(x=>x.Albums)
                .ToListAsync();
        }

        public async Task<Music> GetSingleMusic(int id)
        {
            var Music = await _context.Musics
                .Include(x=>x.Playlists)
                .Include(x=>x.Albums)
                .FirstOrDefaultAsync(u=> u.Id == id) ;
            return Music;
        }

        public async Task<Music> UpdateMusic(int id, CRUDMusicDTO MusicToEdit)
        {
            var playlistListModel = new List<PlayList>();
            var albumListModel = new List<Album>();
            
            if (MusicToEdit.PlaylistId != null)
            {
                var playListList = MusicToEdit.PlaylistId.ToList();
                playlistListModel = await _context.PlayLists.Where(x => playListList.Contains(x.Id)).ToListAsync();
            }
            if (MusicToEdit.AlbumId != null)
            {
                var albumList = MusicToEdit.AlbumId.ToList();
                albumListModel = await _context.Albums.Where(x => albumList.Contains(x.Id)).ToListAsync();
            }
            var music = await _context.Musics
                .Include(x=> x.Playlists)
                .Include(x=>x.Albums)
                .FirstAsync(x=>x.Id==id);
            music.Name = MusicToEdit.Name;
            music.TimeOfPlays = MusicToEdit.TimeOfPlays;
            music.Style = MusicToEdit.Style;
            music.ReleaseDate = MusicToEdit.ReleaseDate;
            music.Albums = albumListModel;
            music.Playlists = playlistListModel;
            
            await _context.SaveChangesAsync();
            return await GetSingleMusic(id);
        }

        public async Task<bool> SetThumbnailMusic(int id, byte[] thumbnail)
        {
            var SpotiDBThumbnail = _context.Musics.Find(id);
            SpotiDBThumbnail.Thumbnail = thumbnail;
            return (await _context.SaveChangesAsync()) == 1;
        }

        public async Task<byte[]> GetThumbnailMusic(int id)
        {
            return (await _context.Musics.FindAsync(id)).Thumbnail;
        }
        public async Task<bool> SetSongMusic(int id, byte[] song)
        {
            var SpotiDBSong = _context.Musics.Find(id);
            SpotiDBSong.song = song;
            return (await _context.SaveChangesAsync()) == 1;
        }

        public async Task<byte[]> GetSongMusic(int id)
        {
            return (await _context.Musics.FindAsync(id)).song;
        }
        public Task<bool> ExistById(int id)
        {
            return _context.Musics.AnyAsync(u => u.Id == id);
        }
    }
}