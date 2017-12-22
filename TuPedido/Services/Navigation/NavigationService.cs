using System.Threading.Tasks;
using Xamarin.Forms;

namespace TuPedido.Services
{
    public class NavigationService<TPublicHome, TPrivateHome> : INavigationService
        where TPublicHome : Page, new()
        where TPrivateHome : Page, new()
    {
        private readonly bool animateNavigation = false;
        private INavigation navigation;
        private Page currentPage;

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

                    if (page is TPublicHome)
                    {
                        App.Current.MainPage = new NavigationPage(page);
                        navigation = App.Current.MainPage.Navigation;
                    }
                    else
                    {
                        await navigation.PushAsync(page, animateNavigation);
                    }
                    
                    var viewModel = page.BindingContext as ViewModels.ViewModelBase;
                    await viewModel?.InitializeAsync(navigationData);

                    if (currentPage is TPublicHome)
                    {
                        navigation.RemovePage(currentPage);
                    }
                    currentPage = page;
                });
            });
        }
        
        public Task BackAsync()
        {
            return Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var mainPage = App.Current.MainPage as NavigationPage;
                    await mainPage?.Navigation.PopAsync(animateNavigation);
                });
            });
        }
    }
}
