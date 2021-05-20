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
        
    }
}