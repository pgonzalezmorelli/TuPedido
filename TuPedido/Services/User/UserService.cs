using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuPedido.Helpers;
using TuPedido.Models;

namespace TuPedido.Services
{
    public class UserService : IUserService
    {
        private readonly IRestClient requestProvider;
        private readonly IConfiguration configuration;

        public UserService(IRestClient requestProvider, IConfiguration configuration)
        {
            this.requestProvider = requestProvider;
            this.configuration = configuration;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var employees = await requestProvider.GetAsync<IEnumerable<Employee>>(configuration.Endpoints.GetUsersEndpoint);

            return employees
                .Where(u => !u.Deleted)
                .Select(u => new User
                {
                    Name = u.Name,
                    Email = u.Mail
                })
                .OrderBy(u => u.Name)
                .ToList();
        }
    }
}
