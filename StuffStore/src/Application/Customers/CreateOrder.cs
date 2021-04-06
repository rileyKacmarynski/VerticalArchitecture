using Application.Abstractions;
using Application.Shared.ResultType;
using Domain.Customers;
using Domain.Customers.Dtos;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Customers
{
    public class CreateOrder
    {
        public class Handler : IUseCaseHandler<UseCase>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly ProductRepository _productRepository;

            public Handler(ICustomerRepository customerRepository, ProductRepository productRepository)
            {
                _customerRepository = customerRepository;
                _productRepository = productRepository;
            }

            public async Task<Result> Handle(UseCase useCase, CancellationToken cancellationToken)
            {
                var customer = await _customerRepository.GetByIdAsync(useCase.CustomerId);

                var productIds = useCase.CartItems.Select(i => i.ProductId);
                var productsInOrder = await _productRepository.GetProductsAsync(productIds);

                var productOrders = useCase.CartItems.ToOrderProducts(productsInOrder);

                customer.CreateOrder(productOrders);

                await _customerRepository.SaveChangesAsync();

                return Result.Ok();
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

    public static class CreateOrderExtensions
    {
        public static IEnumerable<OrderProductDto> ToOrderProducts(
            this IEnumerable<CreateOrder.CartItemDto> cartItems,
            IEnumerable<Product> productsInOrder)
        {
            return cartItems.Select(i =>
            {
                var product = productsInOrder.First(p => p.Id == i.ProductId);

                return new OrderProductDto
                {
                    Price = product.Price,
                    Name = product.Name,
                    Quantity = i.Quantity
                };
            });
        }
    }
}
