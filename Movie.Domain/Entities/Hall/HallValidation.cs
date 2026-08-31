using Movie.Domain.Enums;
using Movie.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Entities.Hall
{
    public sealed partial class Hall
    {
        private static void Validate(
        string name,
        ushort rows,
        ushort columns)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException(
                    "Hall name cannot be empty.");
            }

            if (name.Trim().Length > 100)
            {
                throw new DomainException(
                    "Hall name cannot exceed 100 characters.");
            }

            if (rows == 0)
            {
                throw new DomainException(
                    "Hall must contain at least one row.");
            }

            if (columns == 0)
            {
                throw new DomainException(
                    "Hall must contain at least one seat per row.");
            }

            if (rows > 100)
            {
                throw new DomainException(
                    "Hall cannot contain more than 100 rows.");
            }

            if (columns > 100)
            {
                throw new DomainException(
                    "Hall cannot contain more than 100 seats per row.");
            }
        }
    }
}
