using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Session
{
    public sealed class SessionRescheduledEvent : DomainEvent
    {
        public Guid SessionId { get; }

        public DateTime StartTime { get; }

        public DateTime EndTime { get; }

        public SessionRescheduledEvent(
            Guid sessionId,
            DateTime startTime,
            DateTime endTime)
        {
            SessionId = sessionId;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
