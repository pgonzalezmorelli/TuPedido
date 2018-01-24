using System.Collections.Generic;
using System.Threading.Tasks;
using TuPedido.Models;

namespace TuPedido.Managers
{
    public interface IUserManager
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetCurrentUserAsync();
        Task LoginAsync(User user);
        Task LogoutAsync();
    }
}
