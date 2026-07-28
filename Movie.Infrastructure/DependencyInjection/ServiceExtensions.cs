using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movie.Infrastructure.Persistence.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(
            this IServiceCollection services)
        {
            services.AddScoped<AuditableEntityInterceptor>();

            services.AddScoped<SoftDeleteInterceptor>();

            services.AddScoped<PublishDomainEventsInterceptor>();

            return services;
        }
    }
}
