using System.Collections.Generic;
using System.Threading.Tasks;
using SpotiWish_back.Model;

namespace SpotiWish_back.Repositories.Interface
{
    public interface IUsersRepository
    {
        Task<List<User>> GetAllUser();
    }
}