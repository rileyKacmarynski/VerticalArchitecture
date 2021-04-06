using Application.Abstractions;
using Application.Shared.ResultType;
using Domain.Customers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Customers
{
    public class CreateOrderLong
    {
        public class Handler : IUseCaseHandler<UseCase>
        {
            private readonly IValidator<UseCase> _validator;
            private readonly ICustomerRepository _customerRepository;
            private readonly ProductRepository _productRepository;

            public Handler(IValidator<UseCase> validator, 
                ICustomerRepository customerRepository, 
                ProductRepository productRepository)
            {
                _validator = validator;
                _customerRepository = customerRepository;
                _productRepository = productRepository;
            }

            public async Task<Result> Handle(UseCase useCase, CancellationToken cancellationToken)
            {
                try
                {
                    var validationResult = _validator.Validate(useCase);
                    if (!validationResult.IsValid)
                    {
                        var errors = validationResult.Errors
                            .Select(e => (e.PropertyName, e.ErrorMessage));

                        return Result.Invalid(errors);
                    }

                    var customer = await _customerRepository.GetByIdAsync(useCase.CustomerId);

                    var productIds = useCase.CartItems.Select(i => i.ProductId);
                    var productsInOrder = await _productRepository.GetProductsAsync(productIds);

                    var order = new Order
                    {
                        City = customer.City,
                        State = customer.State,
                        Zip = customer.Zip,
                        Address = customer.Address

                    };

                    foreach (var item in useCase.CartItems)
                    {
                        var product = productsInOrder.First(p => p.Id == item.ProductId);
                        order.OrderProducts.Add(new OrderProduct
                        {
                            Price = product.Price,
                            Quantity = item.Quantity
                        });
                    }

                    order.Total = order.OrderProducts.Sum(o => o.Price * o.Quantity);

                    customer.Orders.Add(order);

                    await _customerRepository.SaveChangesAsync();

                    return Result.Ok();
                }
                catch(Exception ex)
                {
                    return Result.Fail(ex.Message);
                }
            }
        }

        public class UseCaseValidator : AbstractValidator<UseCase>
        {
            public UseCaseValidator()
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

        public record UseCase : IUseCase
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
