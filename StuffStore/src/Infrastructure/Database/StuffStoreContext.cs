using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class StuffStoreContext : DbContext, IStuffStoreContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public StuffStoreContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StuffStoreContext).Assembly);
        }
    }
}
