using Movie.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Entities.Language
{
    public sealed partial class Language
    {
        private static void Validate(
            string name,
            string nativeName,
            string isoCode)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException(
                    "Language name cannot be empty.");

            if (name.Trim().Length > 100)
                throw new DomainException(
                    "Language name cannot exceed 100 characters.");

            if (string.IsNullOrWhiteSpace(nativeName))
                throw new DomainException(
                    "Native language name cannot be empty.");

            if (nativeName.Trim().Length > 100)
                throw new DomainException(
                    "Native language name cannot exceed 100 characters.");

            if (string.IsNullOrWhiteSpace(isoCode))
                throw new DomainException(
                    "ISO code cannot be empty.");

            if (isoCode.Trim().Length is < 2 or > 10)
                throw new DomainException(
                    "ISO code must contain between 2 and 10 characters.");
        }
    }
}
