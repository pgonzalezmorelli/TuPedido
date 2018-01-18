using System;

namespace TuPedido.Models
{
    public class Notification
    {
        public Guid DeviceId { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public string Platform { get; set; }
    }
}
