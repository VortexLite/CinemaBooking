using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Movies
{
    public sealed class MovieRestoredEvent : DomainEvent
    {
        public Guid MovieId { get; }

        public MovieRestoredEvent(Guid movieId)
        {
            MovieId = movieId;
        }
    }
}
