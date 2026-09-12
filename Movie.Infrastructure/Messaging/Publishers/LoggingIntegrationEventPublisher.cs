using Microsoft.Extensions.Logging;
using Movie.Infrastructure.Messaging.Outbox;

namespace Movie.Infrastructure.Messaging.Publishers
{
    public sealed class LoggingIntegrationEventPublisher
        : IIntegrationEventPublisher
    {
        private readonly ILogger<LoggingIntegrationEventPublisher> _logger;

        public LoggingIntegrationEventPublisher(
            ILogger<LoggingIntegrationEventPublisher> logger)
        {
            _logger = logger;
        }

        public Task PublishAsync(string eventName, 
            object integrationEvent, 
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Outbox published event {EventName}: {@Event}",
                eventName, integrationEvent);

            return Task.CompletedTask;
        }
    }
}
