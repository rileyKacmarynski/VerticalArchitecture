using Domain.Customers;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(int customerId);
        Task<int> SaveChangesAsync();
        void Add(Customer customer);
    }
}
