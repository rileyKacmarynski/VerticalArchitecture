using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Database;

namespace Web.Pages.Customers.Edit
{
    public class EditCustomerCommandHandler : IRequestHandler<EditCustomerCommand>
    {
        private readonly SimpleStoreContext _context;

        public EditCustomerCommandHandler(SimpleStoreContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(EditCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.FirstAsync(c => c.Id == command.Id);
            customer.Name = command.Name;
            customer.Email = command.Email;

            await _context.SaveChangesAsync();

            return new Unit();
        }
    }
}
