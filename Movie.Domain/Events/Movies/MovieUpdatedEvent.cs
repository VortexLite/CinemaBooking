using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Movies
{
    public sealed class MovieUpdatedEvent : DomainEvent
    {
        public Guid MovieId { get; }

        public MovieUpdatedEvent(Guid movieId)
        {
            MovieId = movieId;
        }
    }
}
