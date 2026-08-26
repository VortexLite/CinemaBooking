using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Movies
{
    public sealed class MovieReleaseDateChangedEvent : DomainEvent
    {
        public Guid MovieId { get; }

        public DateOnly ReleaseDate { get; }

        public MovieReleaseDateChangedEvent(
            Guid movieId,
            DateOnly releaseDate)
        {
            MovieId = movieId;
            ReleaseDate = releaseDate;
        }
    }
}
