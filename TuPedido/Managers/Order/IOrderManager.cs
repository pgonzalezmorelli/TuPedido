using System.Collections.Generic;
using System.Threading.Tasks;
using TuPedido.Models;

namespace TuPedido.Managers
{
    public interface IOrderManager
    {
        Task<IEnumerable<Order>> GetOrdersAsync();

        Task<Order> SaveOrderAsync(Order order);
    }
}
