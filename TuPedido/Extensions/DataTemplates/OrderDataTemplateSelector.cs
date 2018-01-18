using TuPedido.Models;
using Xamarin.Forms;

namespace TuPedido.Extensions
{
    public class OrderDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PendingOrderTemplate { get; set; }
        public DataTemplate ReceivedOrderTemplate { get; set; }

        public OrderDataTemplateSelector()
        {
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var order = item as Order;
            if (order == null)
                return null;

            return order.Received ? ReceivedOrderTemplate : PendingOrderTemplate;
        }
    }
}
