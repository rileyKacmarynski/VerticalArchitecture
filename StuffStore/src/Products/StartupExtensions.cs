using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Products.Web;

namespace Products
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddProducts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(StartupExtensions).Assembly);

            services.AddControllers()
                .AddApplicationPart(typeof(ProductsController).Assembly);

            return services;
        }
    }
}
