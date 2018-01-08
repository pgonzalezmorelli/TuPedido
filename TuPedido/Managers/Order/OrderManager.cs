using System.Collections.Generic;
using System.Threading.Tasks;
using TuPedido.Models;
using TuPedido.Services;

namespace TuPedido.Managers
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderService orderService;

        public OrderManager(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return orderService.GetOrdersAsync();
        }

        public Task<Order> SaveOrderAsync(Order order)
        {
            return orderService.SaveOrderAsync(order);
        }        
    }
}
