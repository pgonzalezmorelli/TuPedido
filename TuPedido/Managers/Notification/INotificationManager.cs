using System.Threading.Tasks;
using TuPedido.Models;

namespace TuPedido.Managers
{
    public interface INotificationManager
    {
        Task Notify(Order order);
    }
}
