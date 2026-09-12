using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Session
{
    public sealed class SessionRescheduledEvent : DomainEvent
    {
        public Guid SessionId { get; }

        public Guid HallId { get; }

        public DateTime StartTime { get; }

        public DateTime EndTime { get; }

        public SessionRescheduledEvent(
            Guid sessionId,
            Guid hallId,
            DateTime startTime,
            DateTime endTime)
        {
            SessionId = sessionId;
            HallId = hallId;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
