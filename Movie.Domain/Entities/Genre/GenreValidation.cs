using Movie.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Entities.Genre
{
    public sealed partial class Genre
    {
        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Genre name cannot be empty.");

            if (name.Trim().Length > 100)
                throw new DomainException("Genre name cannot exceed 100 characters.");
        }
    }
}
