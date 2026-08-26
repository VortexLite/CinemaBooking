using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Movies
{
    public sealed class MovieLanguageChangedEvent : DomainEvent
    {
        public Guid MovieId { get; }

        public Guid LanguageId { get; }

        public MovieLanguageChangedEvent(
            Guid movieId,
            Guid languageId)
        {
            MovieId = movieId;
            LanguageId = languageId;
        }
    }
}
