using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Movies
{
    public sealed class MoviePublishedEvent : DomainEvent
    {
        public Guid MovieId { get; }
        public string Title { get; }
        public MoviePublishedEvent(Guid movieId, string title)
        {
            MovieId = movieId;
            Title = title;
        }
    }
}
