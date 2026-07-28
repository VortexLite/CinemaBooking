using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Session
{
    public sealed class SessionCreatedEvent : DomainEvent
    {
        public Guid SessionId { get; }
        public SessionCreatedEvent(Guid sessionId)
        {
            SessionId = sessionId;
        }
    }
}
