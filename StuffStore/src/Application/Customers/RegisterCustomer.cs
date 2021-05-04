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
    public class RegisterCustomer
    {
        public class Handler : IUseCaseHandler<Request>
        {
            private readonly ICustomerRepository _customerRepository;

            public Handler(ICustomerRepository customerRepository)
            {
                _customerRepository = customerRepository;
            }

            public async Task<Result> Handle(Request useCase, CancellationToken cancellationToken)
            {
                var customer = new Customer(useCase.Name, useCase.Email);

                _customerRepository.Add(customer);
                await _customerRepository.SaveChangesAsync();

                return Result.Ok();
            }
        }

        public class Request : IUseCase
        {
            public string Name { get; set; }
            public string Email { get; set; }
        }

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(c => c.Email).EmailAddress();
                RuleFor(c => c.Name).NotEmpty();
            }
        }
    }
}
