using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.IntegrationEvents
{
    public abstract class IntegrationEvent
        : IIntegrationEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();

        public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
    }
}
