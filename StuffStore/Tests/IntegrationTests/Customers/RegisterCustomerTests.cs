using Application.Customers;
using Application.Shared.ResultType;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Customers
{
    public class RegisterCustomerTests : IClassFixture<TestApplicationFactory>
    {
        private readonly TestApplication _testApplication;

        public RegisterCustomerTests(TestApplicationFactory testApplicationFactory)
        {
            _testApplication = testApplicationFactory.GetTestApplication();
        }

        [Fact]
        public async Task RegisterCustomer_RequestSuccessful_ReturnsOk()
        {
            var request = new RegisterCustomer.Request
            { 
                Email = "Bob@email.com", Name = "Bob" 
            };

            var result = await _testApplication.SendAsync(request);

            result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task RegisterCustomer_ValidationError_ReturnsInvalidResult()
        {
            var useCase = new RegisterCustomer.Request { Email = "", Name = "" };

            var result = await _testApplication.SendAsync(useCase);

            result.Status.Should().Be(ResultStatuses.Invalid);
            result.ValidationErrors.Should().NotBeEmpty();
        }
    }
}
