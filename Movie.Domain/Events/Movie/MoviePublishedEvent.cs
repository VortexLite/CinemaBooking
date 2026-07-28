using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Movie
{
    public sealed class MoviePublishedEvent : DomainEvent
    {
        public Guid MovieId { get; }
        public MoviePublishedEvent(Guid movieId)
        {
            MovieId = movieId;
        }
    }
}
