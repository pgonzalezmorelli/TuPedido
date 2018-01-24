using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuPedido.Models;

namespace TuPedido.Services
{
    public class UserServiceMock : IUserService
    {
        readonly List<User> users = new List<User>
        {
            new User { Name = "Adrian Claveri" },
            new User { Name = "Sebastián Cabrera" },
            new User { Name = "Rodrigo Pintos" },
            new User { Name = "Pablo González" }
        };

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            await Task.Delay(1500);
            return users;
        }

        public async Task SaveUserAsync(User user)
        {
            await Task.Delay(1500);
            users.RemoveAll(u => u.Name.ToLower() == user.Name.ToLower());
            users.Add(user);
        }
    }
}
