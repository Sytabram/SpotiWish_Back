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

        public async Task<User> CreateUser(CreatUserDTO categoryToCreate)
        {
            var categoryDb = new User();
            categoryDb.Name = categoryToCreate.Name;

            _context.Categories.Add(categoryDb);
            await _context.SaveChangesAsync();

            return categoryDb;
        }

        public Task<List<CategorySummaryViewModel>> SearchUser(string name)
        {
            return _context.Categories.Where(c => string.IsNullOrWhiteSpace(name) || c.Name.Contains(name)).Select(t => 
            new CategorySummaryViewModel()
            {
                Id = t.Id,
                Name = t.Name
            }).ToListAsync();
        }

        public async Task<int> Delete(int id)
        {
            _context.Categories.Remove(await _context.Categories.FirstOrDefaultAsync(c => c.Id == id));
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddMovieToCategory(int categoryId, int movieId)
        {
            var categoryDb = await _context.Categories.Include(t => t.Movies).FirstOrDefaultAsync(c => c.Id == categoryId);

            categoryDb.Movies.Add(_context.Movies.Find(movieId));

            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveMovieFromCategory(int categoryId, int movieId)
        {
            var categoryDb = await _context.Categories.Include(t => t.Movies).FirstOrDefaultAsync(c => c.Id == categoryId);

            categoryDb.Movies.Remove(categoryDb.Movies.First(c => c.Id == movieId));

            return await _context.SaveChangesAsync();
        }

        public Task<bool> ExistsById(int id)
        {
            return _context.Categories.AnyAsync(c => c.Id == id);
        }

        public Task<bool> ExistsByName(string name)
        {
            return _context.Categories.AnyAsync(c => c.Name == name);
        }
    }
}