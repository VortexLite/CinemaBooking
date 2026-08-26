using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movie.Application.Common.Interfaces;
using Movie.Domain.Common;
using Movie.Infrastructure.Messaging.IntegrationEvents;
using Movie.Infrastructure.Messaging.Outbox;
using Movie.Infrastructure.Messaging.Publishers;
using Movie.Infrastructure.Persistence;
using Movie.Infrastructure.Persistence.Interceptors;
using Movie.Infrastructure.Persistence.Outbox;
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

            services.AddSingleton<IDomainEventToIntegrationEventMapper, DomainEventToIntegrationEventMapper>();

            services.AddScoped<PublishDomainEventsInterceptor>();

            //services.AddSingleton<IIntegrationEventPublisher,
            //    LoggingIntegrationEventPublisher>();

            return services;
        }
    }
}
