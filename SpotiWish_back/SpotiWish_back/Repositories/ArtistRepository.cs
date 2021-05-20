using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpotiWish_back.Data;
using SpotiWish_back.Model;
using SpotiWish_back.Repositories.Interface;

namespace SpotiWish_back.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly SpotiWishDataContext _context;

        public ArtistRepository(SpotiWishDataContext context)
        {
            _context = context;
        }
        public async Task<Artist> AddArtist(CRUDArtistDTO newArtist)
        {
            var model = new Artist();
            model.Name = newArtist.Name;
            model.TimeOfHeard = newArtist.TimeOfHeard;
            _context.Artists.Add(model);
            
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<int> DeleteArtist(int id)
        {
            _context.Artists.Remove(await _context.Artists.FindAsync(id));
            await _context.SaveChangesAsync();
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Artist>> GetAllArtist()
        {
            return await _context.Artists
                .ToListAsync();
        }

        public async Task<Artist> GetSingleArtist(int id)
        {
            var Artist = await _context.Artists
                .FirstOrDefaultAsync(u=> u.Id == id) ;
            return Artist;
        }

        public async Task<Artist> UpdateArtist(int id, CRUDArtistDTO ArtistToEdit)
        {
            var model = await _context.Artists.FindAsync(id);
            model.Name = ArtistToEdit.Name;
            model.TimeOfHeard = ArtistToEdit.TimeOfHeard;

            await _context.SaveChangesAsync();
            return await GetSingleArtist(id);
        }

        public async Task<bool> SetProfilThumbnailArtist(int id, byte[] thumbnail)
        {
            var SpotiDB = _context.Artists.Find(id);
            SpotiDB.ProfilThumbnail = thumbnail;
            return (await _context.SaveChangesAsync()) == 1;
        }

        public async Task<byte[]> GetProfilThumbnailArtist(int id)
        {
            return (await _context.Artists.FindAsync(id)).ProfilThumbnail;
        }
        public async Task<bool> SetBackThumbnailArtist(int id, byte[] thumbnail)
        {
            var SpotiDB = _context.Artists.Find(id);
            SpotiDB.BackGroundThumbnail = thumbnail;
            return (await _context.SaveChangesAsync()) == 1;
        }
        public async Task<byte[]> GetBackThumbnailArtist(int id)
        {
            return (await _context.Artists.FindAsync(id)).BackGroundThumbnail;
        }
        public Task<bool> ExistById(int id)
        {
            return _context.Artists.AnyAsync(u => u.Id == id);
        }
    }
}