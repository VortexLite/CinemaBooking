using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Session
{
    public sealed class SessionFinishedEvent : DomainEvent
    {
        public Guid SessionId { get; }

        public SessionFinishedEvent(Guid sessionId)
        {
            SessionId = sessionId;
        }
    }
}
