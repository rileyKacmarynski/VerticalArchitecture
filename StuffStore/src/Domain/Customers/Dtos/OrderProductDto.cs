﻿namespace Domain.Customers.Dtos
{
    public record OrderProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
