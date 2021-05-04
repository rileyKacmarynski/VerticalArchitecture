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
        public CustomerDto Data { get; set; }

        public CreateModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnGetAsync(int customerId)
        {
            var request = new GetCustomer.Request(customerId);
            Data = await _mediator.Send(request);
        }

        public async Task OnPostAsync()
        {
            await _mediator.Send(Data);
        }
    }

    public class CommandHandler : MediatR.IRequestHandler<Command>
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

    public record Command : MediatR.IRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }
    }

    public interface IRequest
    {

    }

    public interface IRequest<TResponse>
    {

    }

    public interface IRequestHandler<TRequest>
        where TRequest : IRequest
    {
        Task Handle(TRequest request);
    }

    public interface IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request);
    }

    public class RegisterCustomerRequest : IRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }

    public class RegisterCustomerRequestHandler
        : IRequestHandler<RegisterCustomerRequest>
    {
        public Task Handle(RegisterCustomerRequest request)
        {
            // write code to register customer here
            return Task.FromResult(0);
        }
    }

    public class GetCustomer
    {
        public record Request(int Id) 
            : MediatR.IRequest<CustomerDto>;

        public class Handler 
            : MediatR.IRequestHandler<Request, CustomerDto>
        {
            public Task<CustomerDto> Handle(Request request, CancellationToken cancellationToken)
            {
                return Task.FromResult(new CustomerDto());
            }
        }
    }

    public class CustomerDto
    {
    }
}
