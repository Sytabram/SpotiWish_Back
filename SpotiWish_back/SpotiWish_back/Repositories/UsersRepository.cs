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
    public class UsersRepository : IUsersRepository
    {
        private readonly SpotiWishDataContext _context;

        public UsersRepository(SpotiWishDataContext context)
        {
            _context = context;
        }
        public async Task<List<PlayList>> GetPlaylistById(List<int> idList)
        {
            return await _context.PlayLists
                .Include(x=>x.Users)
                .Include(x=>x.Musics)
                .Where(t => idList.Contains(t.Id)).ToListAsync();
                
        }
        public async Task<int> DeleteUser(int id)
        {
            _context.Users.Remove(await _context.Users.FindAsync(id));
            await _context.SaveChangesAsync();
            return await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUser()
        {
            return await _context.Users
                .Include(x=> x.Playlists)
                .ToListAsync();
        }

        public async Task<User> GetSingleUser(int id)
        {
            var User = await _context.Users
                .Include(x=> x.Playlists)
                .FirstOrDefaultAsync(u=> u.Id == id) ;
            return User;
        }

        public async Task<User> UpdateUser(int id, CRUDArtistDTO ArtistToEdit)
        {
            var albumListModel = new List<Album>();
            if (ArtistToEdit.AlbumsId != null)
            {
                var albumList = ArtistToEdit.AlbumsId.ToList();
                albumListModel = await _context.Albums.Where(x => albumList.Contains(x.Id)).ToListAsync();
            }

            var artist = await _context.Artists
                .Include(x => x.Albums)
                .FirstAsync(x => x.Id == id);
            artist.Name = ArtistToEdit.Name;
            artist.TimeOfHeard = ArtistToEdit.TimeOfHeard;
            artist.Albums = albumListModel;
            await _context.SaveChangesAsync();
            return await GetSingleUser(id);
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