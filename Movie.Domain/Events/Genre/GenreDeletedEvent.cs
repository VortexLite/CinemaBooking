using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Genre
{
    public sealed class GenreDeletedEvent : DomainEvent
    {
        public Guid GenreId { get; }

        public GenreDeletedEvent(Guid genreId)
        {
            GenreId = genreId;
        }
    }
}
