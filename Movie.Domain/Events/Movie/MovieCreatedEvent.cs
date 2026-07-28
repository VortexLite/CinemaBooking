using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Movie
{
    public sealed class MovieCreatedEvent : DomainEvent
    {
        public Guid MovieId { get; }
        public MovieCreatedEvent(Guid movieId)
        {
            MovieId = movieId;
        }
    }
}
