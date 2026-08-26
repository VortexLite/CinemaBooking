using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movie.Infrastructure.Messaging.Outbox;
using Movie.Infrastructure.Messaging.Outbox.Metrics;
using Movie.Infrastructure.Messaging.RabbitMQ;
using Movie.Infrastructure.Messaging.RabbitMQ.Topology;
using Movie.Infrastructure.Persistence.Interceptors;
using Movie.Infrastructure.Persistence.Outbox;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.DependencyInjection
{
    public static class MessagingExtensions
    {
        public static IServiceCollection AddMessaging(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<ConvertDomainEventsToOutboxMessagesInterceptor>();

            services.AddScoped<IOutboxProcessor, OutboxProcessor>();

            services.AddScoped<IOutboxCleanupService, OutboxCleanupService>();

            services.AddHostedService<OutboxBackgroundService>();

            services.AddHostedService<OutboxCleanupBackgroundService>();

            services.AddSingleton<OutboxMetrics>();

            services.AddSingleton<IOutboxSerializer, OutboxSerializer>();

            services.AddSingleton<IOutboxMessageSerializer, OutboxMessageSerializer>();

            services.AddSingleton<IIntegrationEventSerializer, IntegrationEventSerializer>();

            services.AddSingleton<IRabbitMqTopologyInitializer, RabbitMqTopologyInitializer>();

            services.AddHostedService<RabbitMqTopologyHostedService>();

            services.AddSingleton<IIntegrationEventPublisher, RabbitMqPublisher>();


            services.AddOpenTelemetry()
                .WithMetrics(metrics =>
                {
                    metrics.AddMeter(OutboxMetrics.MeterName);
                });

            return services;
        }
    }
}
