using Application.Abstractions;
using Application.Shared.Exceptions;
using Domain.Customers;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly StuffStoreContext _context;

        public CustomerRepository(StuffStoreContext context)
        {
            _context = context;
        }

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            return customer is null
                ? throw new EntityNotFoundException<Customer>(customerId)
                : customer;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
