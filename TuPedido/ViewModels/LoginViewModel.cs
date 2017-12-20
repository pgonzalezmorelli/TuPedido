﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using TuPedido.Helpers;
using TuPedido.Managers;
using TuPedido.Models;
using TuPedido.Services;
using Xamarin.Forms;

namespace TuPedido.ViewModels
{
    public class LoginViewModel: ViewModelBase
    {
        #region Attributes & Properties

        private readonly IUserManager userManager;
        private readonly INavigationService navigationService;
        private IEnumerable<User> users;
        private User selectedUser;
        
        public IEnumerable<User> Users { set { SetPropertyValue(ref users, value); } get { return users; } }
        public User SelectedUser { set { SetPropertyValue(ref selectedUser, value); } get { return selectedUser; } }
        public ICommand LoginCommand { get; set; }
        
        #endregion

        public LoginViewModel(IUserManager userManager, INavigationService navigationService)
        {
            this.userManager = userManager;
            this.navigationService = navigationService;

            LoginCommand = new Command(async () => await Login());
        }

        public override Task InitializeAsync(object navigationData)
        {
            return LoadUsers();
        }

        private Task LoadUsers()
        {
            return TryExecute(async () =>
            {
                Users = await userManager.GetUsers();
                SelectedUser = null;
            });
        }

        private Task Login()
        {
            return TryExecute(async() =>
            {
                ValidationHelper.Check(SelectedUser != null, "Selecciona un usuario de la lista");

                await userManager.Login(new User
                {
                    Name = SelectedUser.Name,
                    Email = SelectedUser.Email
                });

                await navigationService.NavigateToAsync(new Views.OrdersListView());
            });
        }
    }
}