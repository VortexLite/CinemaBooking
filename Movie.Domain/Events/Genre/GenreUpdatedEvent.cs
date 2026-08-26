using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Genre
{
    public sealed class GenreUpdatedEvent : DomainEvent
    {
        public Guid GenreId { get; }

        public GenreUpdatedEvent(Guid genreId)
        {
            GenreId = genreId;
        }
    }
}
