﻿using System.Threading.Tasks;
using TuPedido.Helpers;
using TuPedido.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TuPedido.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SplashScreen : ContentPage
	{
		public SplashScreen ()
		{
			InitializeComponent ();
            this.InitializeToolbar(false);

            var statusBar = DependencyContainer.Resolve<IStatusBar>();
            Device.BeginInvokeOnMainThread(statusBar.Hide);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            for (int i = 0; i < 3; i++)
            {
                await SplashImage.ScaleTo(1.15, 800, Easing.Linear);
                await SplashImage.ScaleTo(1, 800, Easing.Linear);
            };

            await Task.WhenAll(
                SplashImage.ScaleTo(5, 1200, Easing.Linear),
                SplashGrid.FadeTo(0, 1200)
            );

			var navigationService = DependencyContainer.Resolve<INavigationService>();
            await navigationService.InitializeAsync();

            var statusBar = DependencyContainer.Resolve<IStatusBar>();
            Device.BeginInvokeOnMainThread(statusBar.Show);
        }
    }
}