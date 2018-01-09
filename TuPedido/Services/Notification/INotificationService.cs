using System;
using System.Threading.Tasks;
using TuPedido.Models;

namespace TuPedido.Services
{
    public interface INotificationService
    {
        Task Send(Notification notification);
    }
}
