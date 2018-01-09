using Microsoft.AppCenter.Push;
using Xamarin.Forms;
using System.Linq;

namespace TuPedido.Views
{
    public partial class OrdersListView : ContentPage
	{
		public OrdersListView()
		{
			InitializeComponent();
            this.InitializeToolbar();
            Push.PushNotificationReceived += Push_PushNotificationReceived;
        }

        async void Push_PushNotificationReceived(object sender, PushNotificationReceivedEventArgs e)
        {
            var summary = $@"Push notification received:
    Notification title: {e.Title}
    Message: {e.Message}";

            if (e.CustomData != null)
            {
                summary += $"\n\tCustom data:\n{string.Join("\n\t", e.CustomData.Select(pair => $"{pair.Key}: {pair.Value}"))}";
            }

            await this.DisplayAlert(e.Title, summary, "chausis");
        }
    }
}
