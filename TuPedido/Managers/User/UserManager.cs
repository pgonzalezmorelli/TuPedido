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
        #region Attributes & Properties

        private readonly IUserService userService;
        private readonly IUserRepository userRepository;
        private User loggedUser;

        #endregion Attributes & Properties

        public UserManager(IUserService userService, IUserRepository userRepository)
        {
            this.userService = userService;
            this.userRepository = userRepository;
        }

        public async Task<User> GetCurrentUserAsync()
        {
            loggedUser = loggedUser ?? await userRepository.GetCurrentUser();
            return await Task.FromResult(loggedUser);
        }

        public Task<IEnumerable<User>> GetUsersAsync()
        {
            return userService.GetUsersAsync();
        }

        public Task LoginAsync(User user)
        {
            ValidationHelper.Check(user != null, "User info is null");
            ValidationHelper.Check(!string.IsNullOrEmpty(user.Name), "Username is null or empty");

            var resultado = Task.WhenAll(
                userRepository.AddUser(user),
                userService.SaveUserAsync(user)
            );

            App.CurrentUser = user;
            return resultado;
        }

        public Task LogoutAsync()
        {
            ValidationHelper.Check(App.CurrentUser != null, "No one is logged in");

            var resultado = userRepository.RemoveUser(App.CurrentUser);
            App.CurrentUser = null;
            return resultado;
        }
    }
}
