using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Session
{
    public sealed class SessionBookingOpenedEvent : DomainEvent
    {
        public Guid SessionId { get; }

        public Guid MovieId { get; }

        public Guid HallId { get; }

        public DateTime StartTime { get; }

        public DateTime EndTime { get; }

        public decimal Price { get; }

        public SessionBookingOpenedEvent(
            Guid sessionId,
            Guid movieId,
            Guid hallId,
            DateTime startTime,
            DateTime endTime,
            decimal price)
        {
            SessionId = sessionId;
            MovieId = movieId;
            HallId = hallId;
            StartTime = startTime;
            EndTime = endTime;
            Price = price;
        }
    }
}