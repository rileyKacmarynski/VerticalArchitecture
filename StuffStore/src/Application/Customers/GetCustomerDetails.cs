using Application.Abstractions;
using Application.Shared.ResultType;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Customers
{
    public class GetCustomerDetails
    {
        public class Handler : IUseCaseHandler<Request, CustomerDetailsDto>
        {
            private readonly ICustomerRepository _customerRepository;

            public Handler(ICustomerRepository customerRepository)
            {
                _customerRepository = customerRepository;
            }

            public async Task<Result<CustomerDetailsDto>> Handle(Request useCase, 
                CancellationToken cancellationToken)
            {
                var customer = await _customerRepository.GetByIdAsync(useCase.CustomerId);
                
                return Result.Ok(new CustomerDetailsDto
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Email = customer.Email,
                });
            }
        }

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(u => u.CustomerId).GreaterThan(0);
            }
        }

        public class Request : IUseCase<CustomerDetailsDto>
        {
            public int CustomerId { get; set; }
        }
    }

    public class CustomerDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
