using System.Collections.Generic;
using System.Threading.Tasks;
using TuPedido.Helpers;
using TuPedido.Models;
using TuPedido.Repositories;
using TuPedido.Services;

namespace TuPedido.Managers
{
    public class UserManager : IUserManager
    {
        protected readonly IUserService userService;
        protected readonly IUserRepository userRepository;
        private User loggedUser;

        public UserManager(IUserService userService, IUserRepository userRepository)
        {
            this.userService = userService;
            this.userRepository = userRepository;
        }

        public async Task<User> GetCurrentUser()
        {
            loggedUser = loggedUser ?? await userRepository.GetCurrentUser();
            return await Task.FromResult(loggedUser);
        }

        public Task<IEnumerable<User>> GetUsers()
        {
            return userService.GetUsers();
        }

        public Task Login(User user)
        {
            ValidationHelper.Check(user != null, "User info is null");
            ValidationHelper.Check(!string.IsNullOrEmpty(user.Name), "Username is null or empty");
            
            var resultado = userRepository.AddUser(user);
            App.CurrentUser = user;
            return resultado;
        }

        public Task Logout()
        {
            ValidationHelper.Check(App.CurrentUser != null, "No one is logged in");

            var resultado = userRepository.RemoveUser(App.CurrentUser);
            App.CurrentUser = null;
            return resultado;
        }
    }
}
