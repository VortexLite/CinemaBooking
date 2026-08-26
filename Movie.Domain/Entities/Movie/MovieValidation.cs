using Movie.Domain.Exceptions;
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
                throw new DomainException("Movie title is required.");

            if (title.Length > 200)
                throw new DomainException("Movie title cannot exceed 200 characters.");
        }

        private static void ValidateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new DomainException("Movie description is required.");

            if (description.Length > 3000)
                throw new DomainException("Movie description cannot exceed 3000 characters.");
        }

        private static void ValidateDuration(int durationMinutes)
        {
            if (durationMinutes <= 0)
                throw new DomainException("Movie duration must be greater than zero.");

            if (durationMinutes > 600)
                throw new DomainException("Movie duration cannot exceed 600 minutes.");
        }

        private static void ValidateAgeRestriction(byte ageRestriction)
        {
            if (ageRestriction > 21)
                throw new DomainException("Invalid age restriction.");
        }


        private static void ValidateReleaseDate(DateOnly releaseDate)
        {
            if (releaseDate.Year < 1888)
                throw new DomainException("Release date is invalid.");
        }

        private static void ValidateLanguage(Guid languageId)
        {
            if (languageId == Guid.Empty)
                throw new DomainException("Language is required.");
        }

        private static void ValidateUrl(string url, string propertyName)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out _))
                throw new DomainException($"{propertyName} URL is invalid.");
        }

        private static void ValidatePoster(string posterUrl)
        {
            if (string.IsNullOrWhiteSpace(posterUrl))
                throw new DomainException("Poster URL is required.");

            ValidateUrl(posterUrl, "Poster");
        }

        private static void ValidateTrailer(string? trailerUrl)
        {
            if (string.IsNullOrWhiteSpace(trailerUrl))
                return;

            ValidateUrl(trailerUrl, "Trailer");
        }
    }
}
