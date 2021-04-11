using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Products
{
    public class GetProductTests : IClassFixture<TestApplicationFactory>
    {
        private TestApplication _testApplication;

        public GetProductTests(TestApplicationFactory testApplicationFactory)
        {
            _testApplication = testApplicationFactory.GetTestApplication();
        }

        [Fact]
        public async Task GetProduct_ProductExists_ReturnsResultWithProductDtoValue()
        {

        }
    }
}
