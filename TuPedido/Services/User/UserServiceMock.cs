using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuPedido.Models;

namespace TuPedido.Services
{
    public class UserServiceMock : IUserService
    {
        public Task<IEnumerable<User>> GetUsers()
        {
            Task.Delay(3000).Wait();
            return Task.FromResult((IEnumerable<User>)new List<User>
            {
                new User { Name = "Adrian Claveri" },
                new User { Name = "Sebastián Cabrera" },
                new User { Name = "Rodrigo Pintos" },
                new User { Name = "Pablo González" }
            });
        }
    }
}
