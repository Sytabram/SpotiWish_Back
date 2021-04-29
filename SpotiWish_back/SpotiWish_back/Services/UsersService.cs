using System;
using System.Threading.Tasks;
using SpotiWish_back.Model;
using SpotiWish_back.Repositories.Interface;

namespace SpotiWish_back.Services
{
    public class UsersService
    {
        private readonly IUsersRepository _categoriesRepository;
        public UsersService(IUsersRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }
        public async Task<User> GetSingle(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");

            var CategoryDb = await _categoriesRepository.GetSingle(id);

            return CategoryDb;
        }
    }
}