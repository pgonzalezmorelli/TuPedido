﻿using Xamarin.Forms;

namespace TuPedido.Views
{
    public partial class OrdersListView : ContentPage
	{
		public OrdersListView()
		{
			InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
		}
	}
}
