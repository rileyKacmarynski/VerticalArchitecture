using Api.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public static class Startup
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddControllers()
                .AddApplicationPart(typeof(ProductsController).Assembly);

            return services;
        }
    }
}
