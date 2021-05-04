using Application.Abstractions;
using Application.Customers;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FluentValidation.TestHelper;

namespace UnitTests
{
    public class RegisterCustomerTests
    {
        [Fact]
        public async Task Handler_SaveSuccessful_ReturnsOk()
        {
            // arrange
            var customerRepoMock = new Mock<ICustomerRepository>().Object;
            var useCase = new RegisterCustomer.Request
            {
                Name = "Bob",
                Email = "Bob@email.com"
            };

            // act  Test just the handler
            var handler = new RegisterCustomer.Handler(customerRepoMock);
            var result = await handler.Handle(useCase, CancellationToken.None);

            // assert
            result.Success.Should().BeTrue();
        }

        // handler throws error. pipeline behavior handles it. That will be caught 
        // by an integration test. 
        [Fact]
        public void Handler_SaveFailed_ThrowsException()
        {
            // arrange
            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock.Setup(repo => repo.SaveChangesAsync().Result).Throws<Exception>();

            var useCase = new RegisterCustomer.Request
            {
                Name = "Bob",
                Email = "Bob@email.com"
            };

            // act
            var handler = new RegisterCustomer.Handler(customerRepoMock.Object);
            Func<Task> func = async () => await handler.Handle(useCase, CancellationToken.None);

            // assert
            func.Should().Throw<Exception>();
        }

        [Fact]
        public void Validator_ValidUseCase_NoErrors()
        {
            var useCase = new RegisterCustomer.Request
            {
                Name = "Bob",
                Email = "Bob@email.com"
            };

            var validator = new RegisterCustomer.Validator();
            var result = validator.TestValidate(useCase);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validator_InvalidName_InvalidResult()
        {
            var useCase = new RegisterCustomer.Request
            {
                Name = "",
                Email = "Bob@email.com"
            };

            var validator = new RegisterCustomer.Validator();
            var result = validator.TestValidate(useCase);

            result.ShouldHaveValidationErrorFor(u => u.Name);
        }
    }
}
