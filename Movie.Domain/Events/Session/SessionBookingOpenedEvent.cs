using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Session
{
    public sealed class SessionBookingOpenedEvent : DomainEvent
    {
        public Guid SessionId { get; }
        
        public SessionBookingOpenedEvent(Guid sessionId)
        {
            SessionId = sessionId;
        }
    }
}
