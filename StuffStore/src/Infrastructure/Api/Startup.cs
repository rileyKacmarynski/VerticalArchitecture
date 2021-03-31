using Api.Products;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Builder;
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

        public static IApplicationBuilder UseApi(this IApplicationBuilder app)
        {
            app.UseProblemDetails();

            return app;
        }
    }
}
