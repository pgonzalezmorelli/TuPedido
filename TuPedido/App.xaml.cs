using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

using Xamarin.Forms;

namespace TuPedido
{
    public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            SetupAppCenter();
            MainPage = new TuPedido.Views.MainPage();
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
            var config = DependencyService.Get<IConfiguration>();
            AppCenter.Start(string.Format("ios={0}; android={1}; uwp={2};", config.AppCenter.iOSSecretKey, config.AppCenter.AndroidSecretKey, config.AppCenter.UwpSecretKey),
                   typeof(Analytics), typeof(Crashes));
        }
    }
}
