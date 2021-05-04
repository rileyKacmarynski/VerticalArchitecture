using System.Collections.Generic;

namespace Web.Models
{
    public class Order
    {
        public Order()
        {
            ShippingAddress = new Address();
            OrderItems = new List<OrderItem>();
        }
        public int OrderId { get; set; }
        public decimal Total { get; set; }
        public Address ShippingAddress { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
