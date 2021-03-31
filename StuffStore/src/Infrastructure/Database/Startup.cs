using Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class Startup
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddScoped<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

            return services;
        }
    }
}
