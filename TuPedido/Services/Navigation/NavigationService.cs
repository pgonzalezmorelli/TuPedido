using System.Threading.Tasks;
using TuPedido.Views;
using Xamarin.Forms;

namespace TuPedido.Services
{
    public class NavigationService : INavigationService
    {
        private readonly bool animateNavigation = false;

        public Task InitializeAsync()
        {
            return NavigateToAsync(new LoginView());
        }

        public Task NavigateToAsync(Page page)
        {
            return Task.Run(async () =>
            {
                var isLogin = page is LoginView;
                if (App.CurrentUser != null && isLogin)
                {
                    page = new OrdersListView();
                }

                var mainPage = App.Current.MainPage as NavigationPage;
                var existNavigation = mainPage != null;
                if (existNavigation && !isLogin)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await mainPage.Navigation.PushAsync(page, animateNavigation);
                        var viewModel = page.BindingContext as ViewModels.ViewModelBase;
                        if (viewModel != null)
                        {
                            await viewModel.InitializeAsync(null);
                            //viewModel.CanGoBack = await CanGoBackAsync();
                        }
                    });
                    return;
                };

                await SetMainPage(page, !isLogin && !existNavigation);
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
                    if (viewModel != null)
                    {
                        await viewModel.InitializeAsync(null);
                        //viewModel.CanGoBack = await CanGoBackAsync();
                    }
                });
            });
        }

        public Task BackAsync()
        {
            return Task.Run(async () =>
            {
                if (await CanGoBackAsync())
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        var mainPage = App.Current.MainPage as NavigationPage;
                        await mainPage?.Navigation.PopAsync(animateNavigation);
                    });
                }
            });
        }

        public Task<bool> CanGoBackAsync()
        {
            return Task.Run(() =>
            {
                var mainPage = App.Current.MainPage as NavigationPage;
                return mainPage != null && mainPage.Navigation.NavigationStack.Count > 1;
            });
        }
    }
}
