using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    using Application.Abstractions;
    using Application.Shared.ResultType;
    using Domain.Customers;
    using FluentValidation;
    using global::Web.Database;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    namespace Web.Models
    {
        public class CreateOrder
        {
            public class Handler : IRequestHandler<Request>
            {
                private readonly IValidator<Request> _validator;
                private readonly SimpleStoreContext _dbContext;
                private readonly ILogger<CreateOrder> _logger;

                public Handler(IValidator<Request> validator,
                    SimpleStoreContext dbContext,
                    ILogger<CreateOrder> logger)
                {
                    _validator = validator;
                    _dbContext = dbContext;
                    _logger = logger;
                }

                public async Task Handle(Request request
                    , CancellationToken cancellationToken)
                {

                    var customer = await _dbContext.Customers
                        .FindAsync(request.CustomerId, cancellationToken);

                    var productIds = request.CartItems.Select(i => i.ProductId);
                    var productsInOrder = await _dbContext.Products
                        .Where(p => productIds.Contains(p.Id))
                        .ToListAsync(cancellationToken);

                    customer.CreateOrder(request.CartItems, productsInOrder);

                    await _dbContext.SaveChangesAsync(cancellationToken);
                }

                Task<Unit> IRequestHandler<Request, Unit>.Handle(Request request, CancellationToken cancellationToken)
                {
                    throw new NotImplementedException();
                }
            }

            public class RequestValidator : AbstractValidator<Request>
            {
                public RequestValidator()
                {
                    RuleFor(u => u.CustomerId).NotEmpty();
                    RuleFor(u => u.CartItems).NotEmpty().WithMessage("Must have at least one cart item");

                    RuleForEach(u => u.CartItems).ChildRules(item =>
                    {
                        item.RuleFor(i => i.ProductId).NotEmpty();
                        item.RuleFor(i => i.Quantity).GreaterThan(0);
                    });
                }
            }

            public record Request : IRequest
            {
                public int CustomerId { get; set; }
                public IEnumerable<CartItemDto> CartItems { get; set; }
            }

            public record CartItemDto
            {
                public int ProductId { get; set; }
                public int Quantity { get; set; }
            }
        }
    }

}
