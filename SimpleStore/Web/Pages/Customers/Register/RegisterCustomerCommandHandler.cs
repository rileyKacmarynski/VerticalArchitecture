using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Web.Database;
using Web.Models;

namespace Web.Pages.Customers.Register
{
    public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand>
    {
        private readonly SimpleStoreContext _context;

        public RegisterCustomerCommandHandler(SimpleStoreContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RegisterCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Name = command.Name,
                Email = command.Email
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return new Unit();
        }
    }
}
