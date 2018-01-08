using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TuPedido.Services;
using Xamarin.Forms;

namespace TuPedido.ViewModels
{
    public class ViewModelBase : BindableObject, INotifyPropertyChanged
    {
        #region Attributes & Properties

        protected readonly INavigationService navigationService;
        private bool isBusy;
        private string errorMessage;
        
        public bool IsBusy { set { SetPropertyValue(ref isBusy, value); } get { return isBusy; } }
        public string ErrorMessage { set { SetPropertyValue(ref errorMessage, value); } get { return errorMessage; } }

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

        public ViewModelBase(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        protected async Task TryExecute(Func<Task> execute, Func<Exception, Task> onError = null)
        {
            await Task.Run(async () =>
            {
                if (IsBusy) return;

                IsBusy = true;
                ErrorMessage = string.Empty;

                try
                {
                    await execute();
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    if (onError != null) await onError(ex);
                }
                finally
                {
                    IsBusy = false;
                }
            });
        }
    }
}
