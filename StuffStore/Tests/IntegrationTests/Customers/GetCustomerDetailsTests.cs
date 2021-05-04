using Application.Customers;
using Domain.Customers;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Customers
{
    public class GetCustomerDetailsTests : IClassFixture<TestApplicationFactory>
    {
        private readonly TestApplication _testApplication;

        public GetCustomerDetailsTests(TestApplicationFactory testApplicationFactory)
        {
            _testApplication = testApplicationFactory.GetTestApplication();
        }

        [Fact]
        public async Task GetCustomer_RequestSuccessful_ReturnsOk()
        {
            // arrange
            var customer = new Customer(1, "Bob", "Bob@domain.com");

            await _testApplication.ExecuteDbContextAsync(async context =>
            {
                context.Customers.Add(customer);
                await context.SaveChangesAsync();
            });

            var request = new GetCustomerDetails.Request { CustomerId = 1 };

            // act
            var result = await _testApplication.SendAsync(request);

            // assert
            result.Success.Should().BeTrue();

            var value = result.Get();
            value.Should().BeSameAs(customer);
        }
    }
}
