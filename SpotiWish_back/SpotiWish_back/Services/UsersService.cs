using System;
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
        public async Task<Music> AddUser(CRUDMusicDTO newMusic)
        {
            var modelDb = await _UsersRepository.(newMusic);
            return modelDb;
        }
    }
}