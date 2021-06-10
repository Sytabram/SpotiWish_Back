using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpotiWish_back.Model;
using SpotiWish_back.Repositories.Interface;

namespace SpotiWish_back.Services
{
    public class UsersService
    {
        private readonly IUsersRepository _UsersRepository;
        public UsersService(IUsersRepository usersRepository)
        {
            _UsersRepository = usersRepository;
        }
        
        public async Task<List<User>> GetAllUser()
        {
            return  await _UsersRepository.GetAllUser();
        }

    }
}