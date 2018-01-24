using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TuPedido.Models;

namespace TuPedido.Helpers
{
    public interface IExcelHelper
    {
        IEnumerable<Order> GetOrders(Stream stream);

        Order GetOrder(Stream stream, Guid id);

        byte[] SaveUser(Stream stream, User user);
    }
}
