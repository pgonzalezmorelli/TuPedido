using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuPedido.Models;

namespace TuPedido.Managers
{
    public interface IOrderManager
    {
        Task<Order> GetOrderAsync(Guid id);

        Task<IEnumerable<Order>> GetOrdersAsync();

        Task<Order> SaveOrderAsync(Order order);
    }
}
