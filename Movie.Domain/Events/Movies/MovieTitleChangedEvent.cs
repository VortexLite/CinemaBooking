using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Movies
{
    public sealed class MovieTitleChangedEvent : DomainEvent
    {
        public Guid MovieId { get; }

        public string OldTitle { get; }

        public string NewTitle { get; }

        public MovieTitleChangedEvent(
            Guid movieId,
            string oldTitle,
            string newTitle)
        {
            MovieId = movieId;
            OldTitle = oldTitle;
            NewTitle = newTitle;
        }
    }
}
