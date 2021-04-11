using Domain.Customers.Dtos;
using System;
using System.Collections.Generic;

namespace Domain.Customers
{
    public class Customer
    {
        public Customer(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public Customer(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        private Customer() { }  // for EF

        public int Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string City { get; }
        public string State { get; }
        public string Zip { get; }
        public string Address { get; }
        public decimal Total { get; }
        public ICollection<Order> Orders { get; set; }

        public void CreateOrder(IEnumerable<OrderProductDto> orderProducts)
        {
            var order = new Order(this);

            foreach (var orderProduct in orderProducts)
            {
                order.AddOrderProduct(orderProduct);
            }

            Orders.Add(order);
        }
    }
}
