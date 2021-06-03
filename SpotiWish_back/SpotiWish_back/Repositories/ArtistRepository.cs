using System.Collections.Generic;
using System.Linq;
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
            model.Albums = await GetAlbumsById(newArtist.AlbumsId);
            _context.Artists.Add(model);
            
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<List<Album>> GetAlbumsById(List<int> idList)
        {
            return await _context.Albums
                .Include(x=>x.Artists)
                .Include(x=>x.Musics)
                .Where(t => idList.Contains(t.Id)).ToListAsync();
                
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
                .Include(x=> x.Albums)
                .ToListAsync();
        }

        public async Task<Artist> GetSingleArtist(int id)
        {
            var Artist = await _context.Artists
                .Include(x=> x.Albums)
                .FirstOrDefaultAsync(u=> u.Id == id) ;
            return Artist;
        }

        public async Task<Artist> UpdateArtist(int id, CRUDArtistDTO ArtistToEdit)
        {
            var albumListModel = new List<Album>();
            if (ArtistToEdit.AlbumsId != null)
            {
                var albumList = ArtistToEdit.AlbumsId.ToList();
                albumListModel = await _context.Albums.Where(x => albumList.Contains(x.Id)).ToListAsync();
            }

            var artist = await _context.Artists
                .Include(x => x.Albums).FirstAsync(x => x.Id == id);
            artist.Name = ArtistToEdit.Name;
            artist.TimeOfHeard = ArtistToEdit.TimeOfHeard;
            artist.Albums = albumListModel;
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