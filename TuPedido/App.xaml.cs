using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Push;
using System.Threading.Tasks;
using TuPedido.Helpers;
using TuPedido.Managers;
using TuPedido.Models;
using TuPedido.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TuPedido
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
	{
        public static User CurrentUser { get; set; }

		public App ()
		{
			InitializeComponent();
            SetupAppCenter();
            Initialize();  
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
                typeof(Crashes),
                typeof(Push));
        }

        private void Initialize()
        {
            Task.Run(async () => 
            {
                var userManager = DependencyContainer.Resolve<IUserManager>();
                CurrentUser = await userManager.GetCurrentUser();

                MainPage = new Views.NavigationView(new Views.SplashScreen());
            }).Wait();
        }
    }
}
