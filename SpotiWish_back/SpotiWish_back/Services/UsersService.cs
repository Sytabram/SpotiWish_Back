using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpotiWish_back.Model;
using SpotiWish_back.Repositories.Interface;
using SpotiWish_back.Services.Interface;

namespace SpotiWish_back.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _UsersRepository;
        public UsersService(IUsersRepository UsersRepository)
        {
            _UsersRepository = UsersRepository;
        }
        public async Task<int> DeleteUser(int id)
        {
            if(! await _UsersRepository.ExistById(id))
                throw new NullReferenceException("User doesn't exist");
      
            return await _UsersRepository.DeleteUser(id);
        }

        public async Task<List<User>> GetAllUser()
        {
            return  await _UsersRepository.GetAllUser();
        }

        public async Task<User> GetSingleUser(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id cannot be lower than 1.");
            
            if(! await _UsersRepository.ExistById(id))
                throw new NullReferenceException("User doesn't exist");
        
            
            
            return await _UsersRepository.GetSingleUser(id);
        }

        public async Task<User> UpdateUser(int id, CRUDUserDTO model)
        {
           
            if(! await _UsersRepository.ExistById(id))
                throw new NullReferenceException("User doesn't exist");

            var modelDb =await _UsersRepository.UpdateUser(id, model);
            return modelDb;
        }

        public async Task<bool> SetThumbnailUser(int id, byte[] thumbnail)
        {
            return await _UsersRepository.SetThumbnailUser(id, thumbnail);
        }
        public async Task<byte[]> GetThumbnailUser(int id)
        {
            var ImageDb = await _UsersRepository.GetThumbnailUser(id);
            return ImageDb;
        }
    }
}