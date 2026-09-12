using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Session
{
    public sealed class SessionRestoredEvent : DomainEvent
    {
        public Guid SessionId { get; }
        public SessionRestoredEvent(Guid sessionId)
        {
            SessionId = sessionId;
        }
    }
}
