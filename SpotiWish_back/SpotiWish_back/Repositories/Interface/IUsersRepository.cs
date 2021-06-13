using System.Collections.Generic;
using System.Threading.Tasks;
using SpotiWish_back.Model;

namespace SpotiWish_back.Repositories.Interface
{
    public interface IUsersRepository
    {
        Task<int> DeleteUser(int id);
        Task<List<User>> GetAllUser();
        Task<User> GetSingleUser(int id);
        Task<User> UpdateUser(int id, CRUDUserDTO model);
        Task<bool> SetThumbnailUser(int id, byte[] thumbnail);
        Task<byte[]> GetThumbnailUser(int id);
        Task<bool> ExistById(int id);
    }
}