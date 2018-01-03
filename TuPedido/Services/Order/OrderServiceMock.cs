using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuPedido.Models;

namespace TuPedido.Services
{
    public class OrderServiceMock : IOrderService
    {
        private readonly List<Order> orders = new List<Order>
        {
            new Order
            {
                Id = Guid.Parse("12345678-9012-3456-7890-123456789012"),
                Date = DateTime.Parse("2017-12-27 12:52"),
                Owner = "Pablo González",
                Service = "La Pasiva",
                EstimatedDelayMinutes = 30
            },
            new Order
            {
                Id = Guid.Parse("23456789-0123-4567-8901-234567890123"),
                Date = DateTime.Parse("2017-12-27 12:22"),
                Owner = "Tania Berjis",
                Service = "Centeno",
                EstimatedDelayMinutes = 45
            },
            new Order
            {
                Id = Guid.Parse("34567890-1234-5678-9012-345678901234"),
                Date = DateTime.Parse("2017-12-27 12:01"),
                Owner = "Carloluis Rodriguez",
                Service = "Alex (Cubano)",
                EstimatedDelayMinutes = 60
            },
            new Order
            {
                Id = Guid.Parse("45678901-2345-6789-0123-456789012345"),
                Date = DateTime.Parse("2017-12-20 13:05"),
                Owner = "Santiago González",
                Service = "La Negra Tomasa",
                EstimatedDelayMinutes = 35,
                ReceivedDate = DateTime.Parse("2017-12-20 13:47"),
                NotificationDate = DateTime.Parse("2017-12-20 13:47")
            }
        }; 

        public Task<Order> GetOrder(Guid id)
        {
            return Task.FromResult(orders.FirstOrDefault(o => o.Id == id));
        }

        public Task<IEnumerable<Order>> GetOrders()
        {
            return Task.FromResult(orders.AsEnumerable());
        }

        public async Task<Order> SaveOrder(Order order)
        {
            if (order.Id == Guid.Empty)
            {
                order.Id = Guid.NewGuid();
                orders.Add(order);
                return order;
            }

            var existingOrder = await GetOrder(order.Id);
            existingOrder.Date = order.Date;
            existingOrder.DeviceId = order.DeviceId;
            existingOrder.EstimatedDelayMinutes = order.EstimatedDelayMinutes;
            existingOrder.Owner = order.Owner;
            existingOrder.Service = order.Service;
            existingOrder.ReceivedDate = order.ReceivedDate;
            existingOrder.NotificationDate = order.NotificationDate;

            return existingOrder;
        }
    }
}
