using System.Collections.Generic;
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
            _context.Albums.Add(model);
            
            await _context.SaveChangesAsync();
            return model;
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
                .ToListAsync();
        }

        public async Task<Album> GetSingleAlbum(int id)
        {
            var Album = await _context.Albums
                .FirstOrDefaultAsync(u=> u.Id == id) ;
            return Album;
        }

        public async Task<Album> UpdateAlbum(int id, CRUDAlbumDTO AlbumToEdit)
        {
            var model = await _context.Albums.FindAsync(id);
            model.Name = AlbumToEdit.Name;
            model.TotalHeard = AlbumToEdit.TotalHeard;
            model.YearReleased = AlbumToEdit.YearReleased;
            model.TotalTime = AlbumToEdit.TotalTime;

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