using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movie.Domain.Common;
using Movie.Infrastructure.Persistence;
using Movie.Infrastructure.Persistence.Interceptors;
using Movie.Infrastructure.Persistence.Outbox;
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
            services.AddDbContext<MovieDbContext>((provider, options) =>
            {
                Configure(provider, options, configuration);
            });

            //services.AddDbContextFactory<MovieDbContext>((provider, options) =>
            //{
            //    Configure(provider, options, configuration);
            //});

            services.AddScoped<IUnitOfWork>(provider =>
                    provider.GetRequiredService<MovieDbContext>());

            return services;
        }

        private static void Configure(
            IServiceProvider provider,
            DbContextOptionsBuilder options,
            IConfiguration configuration)
        {
            var environment = provider.GetRequiredService<IHostEnvironment>();

            options.UseNpgsql(configuration.GetConnectionString("MovieDatabase"),
                    npgsql =>
                    {
                        npgsql.EnableRetryOnFailure(
                            5,
                            TimeSpan.FromSeconds(10),
                            null);
                    });

            if (environment.IsDevelopment())
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            }

            options.AddInterceptors(
                provider.GetRequiredService<AuditableEntityInterceptor>(),
                provider.GetRequiredService<SoftDeleteInterceptor>(),
                provider.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>(),
                provider.GetRequiredService<PublishDomainEventsInterceptor>());
        }
    }
}
