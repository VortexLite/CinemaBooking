using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.IntegrationEvents.Sessions
{
    public sealed class SessionHallChangedIntegrationEvent
        : IntegrationEvent
    {
        public Guid SessionId { get; init; }

        public Guid HallId { get; init; }

        public SessionHallChangedIntegrationEvent(
            Guid sessionId,
            Guid hallId)
        {
            SessionId = sessionId;
            HallId = hallId;
        }
    }
}
