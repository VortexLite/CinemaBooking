using Microsoft.Extensions.DependencyInjection;
using Movie.Application.Common.Interfaces;
using Movie.Infrastructure.Messaging.IntegrationEvents;
using Movie.Infrastructure.Persistence.Interceptors;

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

            services.AddSingleton(TimeProvider.System);

            return services;
        }
    }
}
