using Movie.Domain.Common;
using Movie.Infrastructure.Persistence.Outbox;
using Shared.Contracts.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Messaging.Outbox
{
    public interface IOutboxMessageSerializer
    {
        OutboxMessage Serialize(IIntegrationEvent integrationEvent);
    }
}
