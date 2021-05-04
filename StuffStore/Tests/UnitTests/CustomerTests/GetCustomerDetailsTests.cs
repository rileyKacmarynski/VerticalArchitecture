using Application.Abstractions;
using Application.Customers;
using Application.Shared.Exceptions;
using Database;
using Domain.Customers;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class GetCustomerDetailsTests
    {

        [Fact]
        public async Task Handler_CustomerFound_ReturnsOkWithValue()
        {
            // arrange
            var customer = new Customer(1, "Bob", "Bob@email.com");

            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()).Result)
                .Returns(customer);


            var useCase = new GetCustomerDetails.Request { CustomerId = customer.Id };

            // act
            var handler = new GetCustomerDetails.Handler(customerRepoMock.Object);
            var result = await handler.Handle(useCase, CancellationToken.None);

            // assert
            result.Success.Should().BeTrue();

            var value = result.Get();
            value.Id.Should().Be(customer.Id);
            value.Name.Should().Be(customer.Name);
        }

        [Fact]
        public void Handler_CustomerNotFound_ThrowsException()
        {
            // arrange
            var id = 1;

            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()).Result)
                .Throws(new EntityNotFoundException<Customer>(id));

            var useCase = new GetCustomerDetails.Request { CustomerId = id };

            // act
            var handler = new GetCustomerDetails.Handler(customerRepoMock.Object);
            Func<Task> func = async () => await handler.Handle(useCase, CancellationToken.None);

            // assert
            func.Should().ThrowExactly<EntityNotFoundException<Customer>>();
        }
    }
}
