using System;
using System.Collections.Generic;
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
        
        public async Task<List<User>> GetAllUser()
        {
            return await _context.Users
                .Include(x=> x.Playlists)
                .ToListAsync();
        }
   
    }
}