using System.Collections.Generic;

namespace Domain.Customers
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ShoppingCart Cart { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
