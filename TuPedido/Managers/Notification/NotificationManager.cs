using System.Threading.Tasks;
using TuPedido.Helpers;
using TuPedido.Models;
using TuPedido.Services;

namespace TuPedido.Managers
{
    public class NotificationManager : INotificationManager
    {
        private readonly INotificationService notificationService;

        public NotificationManager(INotificationService notificationService)
        {
            this.notificationService = notificationService;    
        }

        public Task Notify(Order order)
        {
            ValidationHelper.Check(order != null, "Order info is null");
            ValidationHelper.Check(order.DeviceId.HasValue && !string.IsNullOrWhiteSpace(order.DevicePlatform), "No existe dispositivo asociado para notificar");

            return notificationService.Send(new Notification
            {
                DeviceId = order.DeviceId.Value,
                Platform = order.DevicePlatform,
                Title = "Llegó tu pedido!",
                Message = $"Tu pedido a {order.Service} llegó!"
            });
        }
    }
}
