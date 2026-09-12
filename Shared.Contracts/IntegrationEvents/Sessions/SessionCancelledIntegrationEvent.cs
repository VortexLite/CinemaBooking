using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.IntegrationEvents.Sessions
{
    public sealed class SessionCancelledIntegrationEvent
        : IntegrationEvent
    {
        public Guid SessionId { get; init; }

        public string Reason { get; init; }

        public SessionCancelledIntegrationEvent(
            Guid sessionId,
            string reason)
        {
            SessionId = sessionId;
            Reason = reason;
        }
    }
}
