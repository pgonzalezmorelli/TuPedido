
using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.AppCenter.Push;
using Splat;

namespace TuPedido.Droid
{
    [Activity(Label = "TuPedido", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            ResolveDependencies();

            var configuration = DependencyContainer.Resolve<TuPedido.Helpers.IConfiguration>();
            Push.SetSenderId(configuration.AppCenter.AndroidSenderId);

            LoadApplication(new App());
            //LoadApplication(UXDivers.Gorilla.Droid.Player.CreateApplication(this,
            //    new UXDivers.Gorilla.Config("Good Gorilla")
            //    .RegisterAssembly(typeof(Syncfusion.SfAutoComplete.XForms.SfAutoComplete).Assembly)
            //    .RegisterAssembly(typeof(TuPedido.Extensions.FontAwesomeIcon).Assembly)));
        }

        private void ResolveDependencies()
        {
            DependencyContainer.Register(new Helpers.FileHelper(), typeof(TuPedido.Helpers.IFileHelper));
            DependencyContainer.Register(new Helpers.StatusBarHelper(), typeof(TuPedido.Helpers.IStatusBar));
            DependencyContainer.RegisterDependencies();
        }

        protected override void OnNewIntent(Android.Content.Intent intent)
        {
            base.OnNewIntent(intent);
            Push.CheckLaunchedFromNotification(this, intent);
        }
    }
}

