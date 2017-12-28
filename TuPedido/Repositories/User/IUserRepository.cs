using System;
using System.Threading.Tasks;
using TuPedido.Models;

namespace TuPedido.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetCurrentUser();
        Task AddUser(User user);
        Task RemoveUser(User user);
    }
}
