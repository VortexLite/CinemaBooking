using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Movies
{
    public sealed class MovieDescriptionChangedEvent : DomainEvent
    {
        public Guid MovieId { get; }

        public string Description { get; }

        public MovieDescriptionChangedEvent(
            Guid movieId,
            string description)
        {
            MovieId = movieId;
            Description = description;
        }
    }
}
