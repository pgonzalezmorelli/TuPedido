using System.Threading.Tasks;
using Xamarin.Forms;

namespace TuPedido.Services
{
    public class NavigationService<TSplash, TPublicHome, TPrivateHome> : INavigationService
        where TPublicHome : Page, new()
        where TPrivateHome : Page, new()
        where TSplash : Page, new()
    {
        private readonly bool animateNavigation = false;
        private INavigation navigation;

        public Task InitializeAsync()
        {
            navigation = App.Current.MainPage?.Navigation;
            return NavigateToAsync(new TPublicHome());
        }

        public Task NavigateToAsync(Page page)
        {
            return InternalNavigateToAsync(page, null);
        }

        public Task NavigateToAsync<T>(Page page, T navigationData)
        {
            return InternalNavigateToAsync(page, navigationData);
        }

        private Task InternalNavigateToAsync(Page page, object navigationData)
        {
            return Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (App.CurrentUser == null && !(page is TPublicHome))
                    {
                        page = new TPublicHome();
                    }
                    else if (App.CurrentUser != null && page is TPublicHome)
                    {
                        page = new TPrivateHome();
                    }

                    var currentPage = (App.Current.MainPage as Views.NavigationView).CurrentPage;
                    if (page is TPublicHome && !(currentPage is TSplash))
                    {
                        App.Current.MainPage = new Views.NavigationView(page);
                        navigation = App.Current.MainPage.Navigation;
                    }
                    else
                    {
                        await navigation.PushAsync(page, animateNavigation);
                        if (currentPage is TSplash || currentPage is TPublicHome)
                        {
                            navigation.RemovePage(currentPage);
                        }
                    }

                    var viewModel = page.BindingContext as ViewModels.ViewModelBase;
                    if (viewModel != null) await viewModel.InitializeAsync(navigationData);
                });
            });
        }
        
        public Task BackAsync()
        {
            return Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await navigation?.PopAsync(animateNavigation);
                });
            });
        }
    }
}
