using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public interface IStuffStoreContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Product> Products { get; set; }
    }
}