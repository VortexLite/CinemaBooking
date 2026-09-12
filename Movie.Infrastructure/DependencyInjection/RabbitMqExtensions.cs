using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movie.Infrastructure.Messaging.Outbox;
using Movie.Infrastructure.Messaging.RabbitMQ;

namespace Movie.Infrastructure.DependencyInjection
{
    public static class RabbitMqExtensions
    {
        public static IServiceCollection AddRabbitMq(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IIntegrationEventPublisher, RabbitMqPublisher>();

            return services;
        }
    }
}
