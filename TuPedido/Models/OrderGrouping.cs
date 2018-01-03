using System.Collections.Generic;

namespace TuPedido.Models
{
    public class OrderGrouping : List<Order>
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsEmpty => this.Count == 0;

        public OrderGrouping(string title, IEnumerable<Order> items)
        {
            this.Title = title;
            this.AddRange(items);
        }

        public OrderGrouping(string title, IEnumerable<Order> items, string emptyMessage) : this(title, items)
        {
            this.Message = emptyMessage;
        }
    }
}
