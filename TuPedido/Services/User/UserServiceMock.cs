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
                new User
                {
                    Name = "Adrian Claveri",
                    Email = "adrian.claveri@uruit.com"
                },
                new User
                {
                    Name = "Sebastián Cabrera",
                    Email = "sebastian.cabrera@uruit.com"
                },
                new User
                {
                    Name = "Rodrigo Pintos",
                    Email = "rodrigo.pintos@uruit.com"
                },
                new User
                {
                    Name = "Pablo González",
                    Email = "pablo.gonzalez@uruit.com"
                }
            });
        }
    }
}
