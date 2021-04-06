using Domain.Customers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Customers
{
    public class Order
    {
        public Order(Customer customer)
        {
            City = customer.City;
            State = customer.State;
            Zip = customer.Zip;
            Address = customer.Address;
            OrderProducts = new List<OrderProduct>();
        }

        public int Id { get; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
        public string City { get; }
        public string State { get; }
        public string Zip { get; }
        public string Address { get; }
        public decimal Total { get; private set; }

        internal void AddOrderProduct(OrderProductDto product)
        {
            OrderProducts.Add(new OrderProduct
            {
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity
            });

            CalculateTotal();
        }

        private void CalculateTotal()
        {
            Total = OrderProducts.Sum(o => o.Price * o.Quantity);
        }
    }
}