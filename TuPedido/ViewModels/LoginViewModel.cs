using Microsoft.AppCenter;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using TuPedido.Helpers;
using TuPedido.Managers;
using TuPedido.Models;
using TuPedido.Services;
using Xamarin.Forms;

namespace TuPedido.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        #region Attributes & Properties

        private readonly IUserManager userManager;
        private IEnumerable<User> users;
        private User selectedUser;

        public IEnumerable<User> Users { set { SetPropertyValue(ref users, value); } get { return users; } }
        public User SelectedUser { set { SetPropertyValue(ref selectedUser, value); } get { return selectedUser; } }
        public ICommand LoginCommand { get; set; }

        #endregion

        public LoginViewModel(IUserManager userManager, INavigationService navigationService) : base(navigationService)
        {
            this.userManager = userManager;

            LoginCommand = new Command(async () => await LoginAsync(SelectedUser));
        }

        public override Task InitializeAsync(object navigationData)
        {
            return LoadUsersAsync();
        }

        private Task LoadUsersAsync()
        {
            return TryExecute(async () =>
            {
                Users = await userManager.GetUsersAsync();
                SelectedUser = null;
            });
        }

        private Task LoginAsync(User user)
        {
            return TryExecute(async () =>
            {
                ValidationHelper.Check(user != null, "Selecciona un usuario de la lista");

                user.DeviceId = await AppCenter.GetInstallIdAsync();
                user.DevicePlatform = Xamarin.Forms.Device.RuntimePlatform;
                await userManager.LoginAsync(user);

                await navigationService.NavigateToAsync(new Views.OrdersListView());
            });
        }
    }
}
