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

        public async Task<User> UpdateUser(int id, CRUDUserDTO UserToEdit)
        {
            var playlistListModel = new List<PlayList>();
            if (UserToEdit.PlaylistsId != null)
            {
                var playlistList = UserToEdit.PlaylistsId.ToList();
                playlistListModel = await _context.PlayLists.Where(x => playlistList.Contains(x.Id)).ToListAsync();
            }

            var user = await _context.Users
                .Include(x => x.Playlists)
                .FirstAsync(x => x.Id == id);
            user.Playlists = playlistListModel;
            await _context.SaveChangesAsync();
            return await GetSingleUser(id);
        }

        public async Task<bool> SetThumbnailUser(int id, byte[] thumbnail)
        {
            var SpotiDB = _context.Users.Find(id);
            SpotiDB.Thumbnail = thumbnail;
            return (await _context.SaveChangesAsync()) == 1;
        }

        public async Task<byte[]> GetThumbnailUser(int id)
        {
            return (await _context.Users.FindAsync(id)).Thumbnail;
        }
        public Task<bool> ExistById(int id)
        {
            return _context.Users.AnyAsync(u => u.Id == id);
        }
    }
}