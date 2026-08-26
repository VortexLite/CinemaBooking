using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Movies
{
    public sealed class MovieGenreRemovedEvent : DomainEvent
    {
        public Guid MovieId { get; }
        public Guid GenreId { get; }
        public MovieGenreRemovedEvent(Guid movieId, Guid genreId)
        {
            MovieId = movieId;
            GenreId = genreId;
        }
    }
}
