using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpotiWish_back.Data;
using SpotiWish_back.Model;

namespace SpotiWish_back.Repositories
{
    public class UsersRepository
    {
        private readonly SpotiWishDataContext _context;

        public UsersRepository(SpotiWishDataContext context)
        {
            _context = context;
        }

        public async Task<User> GetSingle(int id)
        {
            return await _context.Users
                .Include(x => x.Name)
                .Include(x => x.Email)
                .Include(x => x.Playlists)
                .Include(x => x.Subscription)
                .Include(x => x.Thumbnail)
                .Include(x => x.IsAdmin)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> UpdateUser(int id, UserDTO userToUpdate)
        {
            var user = await _context.Users.FirstAsync(c => c.Id == id);

            user.Name = userToUpdate.Name;
            user.Email = userToUpdate.Email;
            user.Password = userToUpdate.Password;


            await _context.SaveChangesAsync();

            return user;
        }
    }
}