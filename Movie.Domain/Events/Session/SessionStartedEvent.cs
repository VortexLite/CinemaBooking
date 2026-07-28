using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Session
{
    public sealed class SessionStartedEvent : DomainEvent
    {
        public Guid SessionId { get; }

        public SessionStartedEvent(Guid sessionId)
        {
            SessionId = sessionId;
        }
    }
}
