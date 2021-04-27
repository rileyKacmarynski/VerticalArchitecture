using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Database;
using Web.Models;

namespace Web.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty]
        public Command Data { get; set; }

        public CreateModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnPostAsync()
        {
            await _mediator.Send(Data);
        }
    }

    public class CommandHandler : IRequestHandler<Command>
    {
        private readonly SimpleStoreContext _context;

        public CommandHandler(SimpleStoreContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = command.Name,
                Price = command.Price
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new Unit();
        }
    }

    public record Command : IRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
