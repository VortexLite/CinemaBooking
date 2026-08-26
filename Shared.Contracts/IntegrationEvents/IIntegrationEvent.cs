using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.IntegrationEvents
{
    public interface IIntegrationEvent
    {
        Guid EventId { get; }

        DateTime OccurredOnUtc { get; }
    }
}
