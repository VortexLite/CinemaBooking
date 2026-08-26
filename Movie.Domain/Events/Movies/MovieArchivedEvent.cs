using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Movies
{
    public sealed class MovieArchivedEvent : DomainEvent
    {
        public Guid MovieId { get; }
        public MovieArchivedEvent(Guid movieId)
        {
            MovieId = movieId;
        }
    }
}
