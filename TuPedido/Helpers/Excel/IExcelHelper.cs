using System.Collections.Generic;
using System.IO;
using TuPedido.Models;

namespace TuPedido.Helpers
{
    public interface IExcelHelper
    {
        IEnumerable<Order> Parse(Stream stream);
    }
}
