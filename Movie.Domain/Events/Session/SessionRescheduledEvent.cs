using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Session
{
    public sealed class SessionRescheduledEvent : DomainEvent
    {
        public Guid SessionId { get; }
        public DateTime OldStartTime { get; }
        public DateTime NewStartTime { get; }
        public SessionRescheduledEvent(Guid sessionId, 
            DateTime oldStartTime, 
            DateTime newStartTime)
        {
            SessionId = sessionId;
            OldStartTime = oldStartTime;
            NewStartTime = newStartTime;
        }
    }
}
