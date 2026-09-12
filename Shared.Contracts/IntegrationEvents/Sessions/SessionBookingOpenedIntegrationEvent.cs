using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.IntegrationEvents.Sessions
{
    public sealed class SessionBookingOpenedIntegrationEvent
        : IntegrationEvent
    {
        public Guid SessionId { get; init; }

        public Guid MovieId { get; init; }

        public Guid HallId { get; init; }

        public DateTime StartTime { get; init; }

        public DateTime EndTime { get; init; }

        public decimal Price { get; init; }

        public SessionBookingOpenedIntegrationEvent(
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
