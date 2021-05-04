using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Web.Database;
using Web.Models;

namespace Web.Pages.Customers.Details
{
    public class GetCustomerDetailsRequestHandler : IRequestHandler<GetCustomerDetailsRequest, Customer>
    {
        private readonly SimpleStoreContext _context;

        public GetCustomerDetailsRequestHandler(SimpleStoreContext context)
        {
            _context = context;
        }

        public async Task<Customer> Handle(GetCustomerDetailsRequest request, CancellationToken cancellationToken)
        {
            return await _context.Customers.FindAsync(request.Id, cancellationToken);
        }
    }
}
