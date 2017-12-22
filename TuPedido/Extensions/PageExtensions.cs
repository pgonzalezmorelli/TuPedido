using System.Threading.Tasks;
using TuPedido;
using TuPedido.Managers;
using TuPedido.Services;

namespace Xamarin.Forms
{
    public static class PageExtensions
    {
        public static void InitializeToolbar(this Page page, bool show = true)
        {
            if (!show)
            {
                NavigationPage.SetHasNavigationBar(page, false);
            }
            else
            {
                page.ToolbarItems.Add(new ToolbarItem
                {
                    Icon = "logout.png",
                    Text = "Logout",
                    Command = new Command(async () => await Logout())
                });
            }
        }

        private static async Task Logout()
        {
            var userManager = DependencyContainer.Resolve<IUserManager>();
            await userManager.Logout();

            var navigationService = DependencyContainer.Resolve<INavigationService>();
            await navigationService.InitializeAsync();
        }
    }
}
