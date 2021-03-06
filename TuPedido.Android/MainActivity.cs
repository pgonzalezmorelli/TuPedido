﻿
using Android.App;
using Android.Content.PM;
using Android.OS;
using Splat;

namespace TuPedido.Droid
{
    [Activity(Label = "TuPedido", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            ResolveDependencies();
            
            LoadApplication(new App());
            //LoadApplication(UXDivers.Gorilla.Droid.Player.CreateApplication(this,
            //    new UXDivers.Gorilla.Config("Good Gorilla")
            //    .RegisterAssembly(typeof(Syncfusion.SfAutoComplete.XForms.SfAutoComplete).Assembly)
            //    .RegisterAssembly(typeof(TuPedido.Extensions.FontAwesomeIcon).Assembly)));
        }

        private void ResolveDependencies()
        {
            DependencyContainer.Register(new Helpers.FileHelper(), typeof(TuPedido.Helpers.IFileHelper));
            DependencyContainer.RegisterDependencies();
        }
    }
}

