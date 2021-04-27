using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Web.Database;

namespace Web.Pages.Customers.Edit
{
    public class EditCustomerQueryHandler : IRequestHandler<EditCustomerQuery, EditCustomerCommand>
    {
        private readonly SimpleStoreContext _context;

        public EditCustomerQueryHandler(SimpleStoreContext context)
        {
            _context = context;
        }

        public async Task<EditCustomerCommand> Handle(EditCustomerQuery query, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.FirstAsync(c => c.Id == query.CustomerId);

            return new EditCustomerCommand
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            };
        }
    }
}
