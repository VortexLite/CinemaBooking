using Movie.Domain.Events.Genre;
using Movie.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Entities.Genre
{
    public sealed partial class Genre
    {
        public void Update(string name)
        {
            ValidateName(name);

            string normalizedName = name.Trim();

            if (Name == normalizedName)
                return;

            Name = normalizedName;

            Raise(new GenreUpdatedEvent(Id));
        }

        public void Delete()
        {
            if (IsDeleted)
                throw new DomainException("Genre is already deleted.");

            MarkDeleted();

            Raise(new GenreDeletedEvent(Id));
        }

        public void Restore()
        {
            if (!IsDeleted)
                throw new DomainException("Genre is not deleted.");

            MarkRestored();

            Raise(new GenreRestoredEvent(Id));
        }
    }
}
