using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TuPedido.Helpers;
using TuPedido.Managers;
using TuPedido.Models;
using Xamarin.Forms;

namespace TuPedido.ViewModels
{
    public class OrderDetailViewModel : ViewModelBase
    {
        #region Attributes & Properties

        private readonly IOrderManager orderManager;
		private readonly INotificationManager notificationManager;
        private Order order;

        public Order Order { set { SetPropertyValue(ref order, value); } get { return order; } }
        public ICommand NotifyCommand { get; set; }
              
        #endregion

        public OrderDetailViewModel(IOrderManager orderManager, INotificationManager notificationManager) : base(null)
        {
            this.orderManager = orderManager;
            this.notificationManager = notificationManager;

            IsVisible = false;
            NotifyCommand = new Command<Order>(async (Order item) => await NotifyAsync(item));
        }

        public override Task InitializeAsync(object navigationData)
        {
            IsVisible = false;
            return TryExecute(async() => 
            {
                var orderId = navigationData as Guid?;
                ValidationHelper.Check(orderId != null && orderId.HasValue, "Oops... no se puede ver el detalle del pedido");

                Order = await orderManager.GetOrderAsync(orderId.Value);
                IsVisible = Order != null;
            });
        }

        private Task NotifyAsync(Order item)
        {
            return TryExecute(() => notificationManager.Notify(item));
        }
    }
}
