using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuPedido.Models;

namespace TuPedido.Services
{
    public interface IOrderService
    {
        Task<Order> GetOrder(Guid id);

        Task<IEnumerable<Order>> GetOrders();
        
        Task<Order> SaveOrder(Order order);
    }
}
