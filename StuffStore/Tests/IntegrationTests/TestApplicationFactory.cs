using Database;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class TestApplicationFactory
    {
        private IConfigurationRoot _configuration;

        public TestApplicationFactory()
        {
            _configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public TestApplication GetTestApplication(Action<IServiceCollection> configureServices)
        {
            var startup = new Startup.Startup(_configuration);

            var services = new ServiceCollection();
            startup.ConfigureServices(services);

            // can add test services that take the place of already registered
            // prod services here. 
            configureServices(services);

            var serviceProvider = services.BuildServiceProvider();
            var scopeFactory = serviceProvider.GetService<IServiceScopeFactory>();

            return new TestApplication(_configuration, scopeFactory);
        }

        public TestApplication GetTestApplication() => GetTestApplication(_ => { });
    }

    public class TestApplication
    {
        private IConfigurationRoot _configuration;
        private IServiceScopeFactory _scopeFactory;

        public TestApplication(IConfigurationRoot configuration, IServiceScopeFactory scopeFactory)
        {
            _configuration = configuration;
            _scopeFactory = scopeFactory;
        }

        public async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
        {
            using var scope = _scopeFactory.CreateScope();
            await action(scope.ServiceProvider);
        }

        public async Task ExecuteDbContextAsync(Func<StuffStoreContext, Task> action) =>
            await ExecuteScopeAsync(sp => action(sp.GetRequiredService<StuffStoreContext>()));

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            var response = default(TResponse);
            await ExecuteScopeAsync(async sp =>
            {
                var mediator = sp.GetRequiredService<IMediator>();

                response = await mediator.Send(request);
            });

            return response;
        }
    }
}
