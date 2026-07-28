using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movie.Infrastructure.Persistence;
using Movie.Infrastructure.Persistence.Interceptors;
using Movie.Infrastructure.Persistence.Seed;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.DependencyInjection
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContextPool<MovieDbContext>((provider, options) =>
            {
                var environment = provider.GetRequiredService<IHostEnvironment>();

                options.UseNpgsql(configuration.GetConnectionString("MovieDatabase"));

                if (environment.IsDevelopment()) 
                {
                    options.EnableSensitiveDataLogging();

                    options.EnableDetailedErrors();
                }

                options.AddInterceptors(
                    provider.GetRequiredService<AuditableEntityInterceptor>(),
                    provider.GetRequiredService<SoftDeleteInterceptor>(),
                    provider.GetRequiredService<PublishDomainEventsInterceptor>());
            });

            return services;
        }
    }
}
