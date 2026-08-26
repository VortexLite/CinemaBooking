using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Movies
{
    public sealed class MovieDeletedEvent : DomainEvent
    {
        public Guid MovieId { get; }

        public MovieDeletedEvent(Guid movieId)
        {
            MovieId = movieId;
        }
    }
}
