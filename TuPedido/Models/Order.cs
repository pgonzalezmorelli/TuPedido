using System;

namespace TuPedido.Models
{
    public class Order : EntityBase
    {
        public string Owner { get; set; }
        public DateTime Date { get; set; }
        public string Service { get; set; }
        public Guid? DeviceId { get; set; }
        public int? EstimatedDelayMinutes { get; set; }
        public DateTime? NotificationDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public bool Received => ReceivedDate.HasValue;
        public bool Notified => NotificationDate.HasValue;
        public string DevicePlatform { get; set; }
    }
}
