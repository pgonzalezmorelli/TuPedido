using System.Threading.Tasks;
using TuPedido.Views;
using Xamarin.Forms;

namespace TuPedido.Services
{
    public class NavigationService : INavigationService
    {
        public Task InitializeAsync()
        {
            return NavigateToAsync(new LoginView());
        }

        public Task NavigateToAsync(Page page)
        {
            return Task.Run(async () =>
            {
                if (App.CurrentUser != null && page is LoginView)
                {
                    page = new OrdersListView();
                }

                var mainPage = App.Current.MainPage as NavigationPage;
                if (mainPage != null)
                {
                    Device.BeginInvokeOnMainThread(async () => await mainPage.Navigation.PushAsync(page, true));
                    return;
                };

                await SetMainPage(page, !(page is LoginView) && mainPage == null);
            });
        }

        private Task SetMainPage(Page page, bool createNavigation)
        {
            return Task.Run(() => 
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    App.Current.MainPage = createNavigation ? new NavigationPage(page) : page;
                    var viewModel = page.BindingContext as ViewModels.ViewModelBase;
                    await viewModel?.InitializeAsync(null);
                });
            });
        }

        public Task BackAsync()
        {
            var mainPage = App.Current.MainPage as NavigationPage;
            return mainPage?.Navigation.PopAsync(true);
        }
    }
}
