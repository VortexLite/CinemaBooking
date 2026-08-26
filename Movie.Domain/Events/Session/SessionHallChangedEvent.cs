using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Session
{
    public sealed class SessionHallChangedEvent : DomainEvent
    {
        public Guid SessionId { get; }

        public Guid HallId { get; }

        public SessionHallChangedEvent(
            Guid sessionId,
            Guid hallId)
        {
            SessionId = sessionId;
            HallId = hallId;
        }
    }
}
