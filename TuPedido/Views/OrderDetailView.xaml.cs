using Xamarin.Forms;

namespace TuPedido.Views
{
    public partial class OrderDetailView : ContentPage
	{
		public OrderDetailView()
		{
			InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
		}
	}
}
