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
        
    }
}