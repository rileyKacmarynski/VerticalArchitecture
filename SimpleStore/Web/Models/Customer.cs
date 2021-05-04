using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Web.Models.Web.Models.CreateOrder;

namespace Web.Models
{
    public class Customer
    {
        public Customer()
        {
            Orders = new List<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<Order> Orders { get; set; }
        public Address Address { get; internal set; }

        public void CreateOrder(IEnumerable<CartItemDto> cartItems, List<Product> productsInOrder)
        {
            var order = new Order
            {
                ShippingAddress = Address
            };

            foreach (var item in cartItems)
            {
                var product = productsInOrder.First(p => p.Id == item.ProductId);
                order.OrderItems.Add(new OrderItem
                {
                    Price = product.Price,
                    Quantity = item.Quantity
                });
            }

            order.Total = order.OrderItems.Sum(o => o.Price * o.Quantity);

            Orders.Add(order);
        }
    }
}
