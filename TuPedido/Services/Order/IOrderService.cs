using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuPedido.Models;

namespace TuPedido.Services
{
    public interface IOrderService
    {
        Task<Order> GetOrderAsync(Guid id);

        Task<IEnumerable<Order>> GetOrdersAsync();
        
        Task<Order> SaveOrderAsync(Order order);
    }
}
