using Movie.Infrastructure.Persistence.Outbox;
using Shared.Contracts.IntegrationEvents;

namespace Movie.Infrastructure.Messaging.Outbox
{
    public interface IOutboxMessageSerializer
    {
        OutboxMessage Serialize(IIntegrationEvent integrationEvent);
    }
}
