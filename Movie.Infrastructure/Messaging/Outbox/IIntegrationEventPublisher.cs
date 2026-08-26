using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Messaging.Outbox
{
    public interface IIntegrationEventPublisher
    {
        Task PublishAsync(
            string eventName,
            object integrationEvent,
            CancellationToken cancellationToken = default);
    }
}
