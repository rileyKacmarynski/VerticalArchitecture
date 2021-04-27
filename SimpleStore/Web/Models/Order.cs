using System.Collections.Generic;

namespace Web.Models
{
    public class Order
    {
        public Order()
        {
            Address = new Address();
            OrderItems = new List<OrderItem>();
        }
        public int OrderId { get; set; }
        public decimal OrderTotal { get; set; }
        public Address Address { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
