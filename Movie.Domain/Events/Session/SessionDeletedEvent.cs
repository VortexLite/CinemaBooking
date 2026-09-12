using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Session
{
    public sealed class SessionDeletedEvent : DomainEvent
    {
        public Guid SessionId { get; }
        public SessionDeletedEvent(Guid sessionId)
        {
            SessionId = sessionId;
        }
    }
}
