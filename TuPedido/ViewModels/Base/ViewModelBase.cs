﻿using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using TuPedido.Managers;
using TuPedido.Services;
using Xamarin.Forms;

namespace TuPedido.ViewModels
{
    public class ViewModelBase : BindableObject, INotifyPropertyChanged
    {
        #region Attributes & Properties

        private readonly IUserManager userManager;
        private readonly INavigationService navigationService;
        private bool isBusy;
        private string errorMessage;
        private bool canGoBack;

        public bool IsBusy { set { SetPropertyValue(ref isBusy, value); } get { return isBusy; } }
        public string ErrorMessage { set { SetPropertyValue(ref errorMessage, value); } get { return errorMessage; } }
        public bool CanGoBack { set { SetPropertyValue(ref canGoBack, value); } get { return canGoBack; } }
        public string Username => App.CurrentUser?.Name;

        public ICommand LogoutCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        #endregion

        #region PropertyChanged

        public new event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected bool SetPropertyValue<T>(ref T storageField, T newValue, Expression<Func<T>> propExpr)
        {
            if (Equals(storageField, newValue))
            {
                return false;
            }

            storageField = newValue;
            var prop = (System.Reflection.PropertyInfo)((MemberExpression)propExpr.Body).Member;
            this.RaisePropertyChanged(prop.Name);

            return true;
        }

        protected bool SetPropertyValue<T>(ref T storageField, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (Equals(storageField, newValue))
            {
                return false;
            }

            storageField = newValue;
            this.RaisePropertyChanged(propertyName);

            return true;
        }

        protected void RaiseAllPropertiesChanged()
        {
            // By convention, an empty string indicates all properties are invalid.
            this.PropertyChanged(this, new PropertyChangedEventArgs(string.Empty));
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propExpr)
        {
            var prop = (System.Reflection.PropertyInfo)((MemberExpression)propExpr.Body).Member;
            this.RaisePropertyChanged(prop.Name);
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public ViewModelBase()
        {
            userManager = DependencyContainer.Resolve<IUserManager>();
            navigationService = DependencyContainer.Resolve<INavigationService>();

            Initialize();
        }

        private void Initialize()
        {
            LogoutCommand = new Command(async () => await Logout());
            GoBackCommand = new Command(async () => await GoBack());
        }

        private Task Logout()
        {
            return TryExecute(async () =>
            {
                await userManager.Logout();
                await navigationService.InitializeAsync();
            });
        }

        private Task GoBack()
        {
            return TryExecute(async () =>
            {
                await navigationService.BackAsync();
            });
        }

        public virtual async Task InitializeAsync(object navigationData)
        {
            canGoBack = await navigationService.CanGoBackAsync();
            await Task.FromResult(false);
        }

        protected async Task TryExecute(Func<Task> execute, Func<Task> onError = null)
        {
            await Task.Run(async () =>
            {
                IsBusy = true;
                ErrorMessage = string.Empty;

                try
                {
                    await execute();
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    if (onError != null) await onError();
                }
                finally
                {
                    IsBusy = false;
                }
            });
        }
    }
}
