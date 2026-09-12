using Movie.Infrastructure.Messaging.IntegrationEvents;
using Movie.Infrastructure.Persistence.Outbox;
using Shared.Contracts.IntegrationEvents;

namespace Movie.Infrastructure.Messaging.Outbox
{
    public sealed class OutboxMessageSerializer
        : IOutboxMessageSerializer
    {
        private IOutboxSerializer _serializer;

        public OutboxMessageSerializer(IOutboxSerializer serializer)
        {
            _serializer = serializer;
        }

        public OutboxMessage Serialize(IIntegrationEvent integrationEvent)
        {
            byte[] bytes = _serializer.Serialize(
                integrationEvent,
                integrationEvent.GetType());

            return new OutboxMessage(
                integrationEvent.EventId,
                IntegrationEventRegistry.GetName(integrationEvent.GetType()),
                Convert.ToBase64String(bytes),
                integrationEvent.OccurredOnUtc);
        }
    }
}
