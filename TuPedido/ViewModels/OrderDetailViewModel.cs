using System;
using System.Threading.Tasks;
using TuPedido.Helpers;
using TuPedido.Managers;
using TuPedido.Models;

namespace TuPedido.ViewModels
{
    public class OrderDetailViewModel : ViewModelBase
    {
        #region Attributes & Properties

        private readonly IOrderManager orderManager;
        private Order order;

        public Order Order { set { SetPropertyValue(ref order, value); } get { return order; } }
        
        #endregion

        public OrderDetailViewModel(IOrderManager orderManager) : base(null)
        {
            this.orderManager = orderManager;
        }

        public override Task InitializeAsync(object navigationData)
        {
            base.InitializeAsync(navigationData);

            return TryExecute(async() => 
            {
                var orderId = navigationData as Guid?;
                ValidationHelper.Check(orderId != null && orderId.HasValue, "Oops... no se puede ver el detalle del pedido");

                Order = await orderManager.GetOrderAsync(orderId.Value);
            });
        }
    }
}
