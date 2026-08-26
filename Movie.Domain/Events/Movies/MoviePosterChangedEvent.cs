using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Movies
{
    public sealed class MoviePosterChangedEvent : DomainEvent
    {
        public Guid MovieId { get; }
        public string PosterUrl { get; }
        public MoviePosterChangedEvent(Guid movieId, string posterUrl)
        {
            MovieId = movieId;
            PosterUrl = posterUrl;
        }
    }
}
