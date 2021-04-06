using Domain.Customers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(IEnumerable<int> productIds);
    }
}
