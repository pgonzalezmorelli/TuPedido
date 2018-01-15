using System.Linq;
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
                var userNames = App.CurrentUser.Name.Split(new char[] { ' ' });
                var displayName = (userNames.Length > 2 ? $"{userNames[0]} {userNames[1]}" : userNames[0]) + $" {userNames.Last().Substring(0,1)}.";
                
                page.ToolbarItems.Add(new ToolbarItem { Icon = "logo.png" });
                page.ToolbarItems.Add(new ToolbarItem { Text = displayName });
                page.ToolbarItems.Add(new ToolbarItem
                {
                    Icon = "logout.png",
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
