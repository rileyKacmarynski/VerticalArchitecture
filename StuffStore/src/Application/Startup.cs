using Application.Products.GetProduct;
using Application.Shared.Pipeline;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public static class Startup
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddUseCases();
            return services;
        }

        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup).Assembly);

            // the code above registers request handler classes as IRequestHandler.
            // we want to register them as IUseCaseHandler
            // in case there's the need to create pipeline behaviors for IUseCaseHandler
            // this also allows us to create another interface that uses mediatr and 
            // requires a different pipeline
            services.Scan(scan => scan
                .FromAssemblyOf<GetProduct.Handler>()
                .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
                .AsImplementedInterfaces());

            services.Scan(scan => scan
                .FromAssemblyOf<GetProduct.Handler>()
                .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<>)))
                .AsImplementedInterfaces());

            services.AddPipelineBehavior(typeof(LogginBehavior<,>));
            services.AddPipelineBehavior(typeof(ValidationBehavior<,>));

            services.AddMediatR(typeof(Startup).Assembly);
            // Order matters!!!
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LogginBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }

        private static IServiceCollection AddPipelineBehavior(this IServiceCollection services, Type t)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), t);
            return services;
        }
    }
}
