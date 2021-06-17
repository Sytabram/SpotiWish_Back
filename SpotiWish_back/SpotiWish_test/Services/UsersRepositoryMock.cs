using System.Collections.Generic;
using System.Threading.Tasks;
using SpotiWish_back.Model;
using SpotiWish_back.Repositories.Interface;

namespace SpotiWish_test.Services
{
    public class UsersRepositoryMock : IUsersRepository
    {
        public Task<User> GetSingleUser(int id)
        {
            return Task.FromResult(new User() 
            {
                Id = id,
                UserName = "T1"
            });
        }

        public Task<int> DeleteUser(int id)
        {
            return Task.FromResult(1);
        }

        public Task<bool> ExistById(int id)
        {
            return Task.FromResult(true);
        }

        public Task<byte[]> GetThumbnailUser(int id)
        {
            return null;
        }
        

        public Task<bool> SetThumbnailUser(int id, byte[] thumbnail)
        {
            return Task.FromResult(true);
        }

        public Task<List<User>> GetAllUser()
        {
            return null;
        }

        public Task<User> UpdateUser(int id, CRUDUserDTO teamToUpdate)
        {
            return null;
        }
    }
}