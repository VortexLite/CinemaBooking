using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Genre
{
    public sealed class GenreRestoredEvent : DomainEvent
    {
        public Guid GenreId { get; }

        public GenreRestoredEvent(Guid genreId)
        {
            GenreId = genreId;
        }
    }
}
