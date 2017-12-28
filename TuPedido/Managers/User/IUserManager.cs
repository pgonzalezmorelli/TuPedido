using System.Collections.Generic;
using System.Threading.Tasks;
using TuPedido.Models;

namespace TuPedido.Managers
{
    public interface IUserManager
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetCurrentUser();
        Task Login(User user);
        Task Logout();
    }
}
