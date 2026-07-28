using Movie.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Entities.Movie
{
    public sealed partial class Movie
    {
        private static void ValidateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new Domain.Extensions.DomainException("Movie title is required.");

            if (title.Length > 200)
                throw new DomainException("Movie title is too long.");
        }

        private static void ValidateDuration(int duration)
        {
            if (duration <= 0)
                throw new DomainException("Movie duration must be greater than zero.");
        }

        private static void ValidatePoster(string poster)
        {
            if (string.IsNullOrWhiteSpace(poster))
                throw new DomainException("Poster is required.");
        }
    }
}
