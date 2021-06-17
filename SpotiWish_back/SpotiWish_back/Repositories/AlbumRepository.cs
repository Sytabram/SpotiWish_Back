using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpotiWish_back.Data;
using SpotiWish_back.Model;
using SpotiWish_back.Repositories.Interface;

namespace SpotiWish_back.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly SpotiWishDataContext _context;

        public AlbumRepository(SpotiWishDataContext context)
        {
            _context = context;
        }
        public async Task<Album> AddAlbum(CRUDAlbumDTO newAlbum)
        {
            var model = new Album();
            model.Name = newAlbum.Name;
            model.TotalHeard = newAlbum.TotalHeard;
            model.YearReleased = newAlbum.YearReleased;
            model.TotalTime = newAlbum.TotalTime;
            model.Artists = await GetArtistById(newAlbum.ArtistId);
            model.Musics = await GetMusicById(newAlbum.MusicId);
            _context.Albums.Add(model);
            
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<List<Artist>> GetArtistById(List<int> idList)
        {
            return await _context.Artists
                .Include(x=>x.Albums)
                .Where(t => idList.Contains(t.Id)).ToListAsync();
                
        }
        public async Task<List<Music>> GetMusicById(List<int> idList)
        {
            return await _context.Musics
                .Include(x=>x.Albums)
                .Include(x=>x.Playlists)
                .Where(t => idList.Contains(t.Id)).ToListAsync();
            
        }
        public async Task<int> DeleteAlbum(int id)
        {
            _context.Albums.Remove(await _context.Albums.FindAsync(id));
            await _context.SaveChangesAsync();
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Album>> GetAllAlbum()
        {
            return await _context.Albums
                .Include(x=>x.Artists)
                .Include(x=>x.Musics)
                .ToListAsync();
        }

        public async Task<Album> GetSingleAlbum(int id)
        {
            var Album = await _context.Albums
                .Include(x=>x.Artists)
                .Include(x=>x.Musics)
                .FirstOrDefaultAsync(u=> u.Id == id) ;
            return Album;
        }

        public async Task<Album> UpdateAlbum(int id, CRUDAlbumDTO AlbumToEdit)
        {
            var artistListModel = new List<Artist>();
            var musicListModel = new List<Music>();
            
            if (AlbumToEdit.ArtistId != null)
            {
                var artistList = AlbumToEdit.ArtistId.ToList();
                artistListModel = await _context.Artists.Where(x => artistList.Contains(x.Id)).ToListAsync();
            }
            if (AlbumToEdit.MusicId != null)
            {
                var musicList = AlbumToEdit.MusicId.ToList();
                musicListModel = await _context.Musics.Where(x => musicList.Contains(x.Id)).ToListAsync();
            }
            
            var album = await _context.Albums
                .Include(x=> x.Artists)
                .Include(x=>x.Musics)
                .FirstAsync(x=>x.Id==id);
            album.Name = AlbumToEdit.Name;
            album.TotalHeard = AlbumToEdit.TotalHeard;
            album.YearReleased = AlbumToEdit.YearReleased;
            album.TotalTime = AlbumToEdit.TotalTime;
            album.Artists = artistListModel;
            album.Musics = musicListModel;
            await _context.SaveChangesAsync();
            return await GetSingleAlbum(id);
        }

        public async Task<bool> SetThumbnailAlbum(int id, byte[] thumbnail)
        {
            var SpotiDB = _context.Albums.Find(id);
            SpotiDB.Thumbnail = thumbnail;
            return (await _context.SaveChangesAsync()) == 1;
        }

        public async Task<byte[]> GetThumbnailAlbum(int id)
        {
            return (await _context.Albums.FindAsync(id)).Thumbnail;
        }
        public Task<bool> ExistById(int id)
        {
            return _context.Albums.AnyAsync(u => u.Id == id);
        }
    }
}