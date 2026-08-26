using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Movies
{
    public sealed class MovieDurationChangedEvent : DomainEvent
    {
        public Guid MovieId { get; }

        public int DurationMinutes { get; }

        public MovieDurationChangedEvent(
            Guid movieId,
            int durationMinutes)
        {
            MovieId = movieId;
            DurationMinutes = durationMinutes;
        }
    }
}
