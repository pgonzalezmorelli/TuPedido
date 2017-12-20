using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using TuPedido.Helpers;
using TuPedido.Models;
using TuPedido.Services;
using Xamarin.Forms;

namespace TuPedido
{
    public partial class App : Application
	{
        public static User CurrentUser { get; set; }

		public App ()
		{
			InitializeComponent();
            SetupAppCenter();

            var navigation = DependencyContainer.Resolve<INavigationService>();
            navigation.InitializeAsync();
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        private void SetupAppCenter()
        {
            var config = DependencyContainer.Resolve<IConfiguration>();
            AppCenter.Start(string.Format("ios={0}; android={1}; uwp={2};", 
                config.AppCenter.iOSSecretKey, 
                config.AppCenter.AndroidSecretKey, 
                config.AppCenter.UwpSecretKey),
                typeof(Analytics), 
                typeof(Crashes));
        }
    }
}
