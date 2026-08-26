using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Genre
{
    public sealed class GenreCreatedEvent : DomainEvent
    {
        public Guid GenreId { get; }

        public GenreCreatedEvent(Guid genreId)
        {
            GenreId = genreId;
        }
    }
}
