using Movie.Domain.Enums;
using Movie.Domain.Events.Movies;
using Movie.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Entities.Movie
{
    public sealed partial class Movie
    {

        public void Update(
            string title,
            string description,
            int durationMinutes,
            byte ageRestriction,
            string posterUrl,
            string? trailerUrl,
            DateOnly releaseDate,
            Guid languageId)
        {
            EnsureCanBeModified();

            ChangeTitle(title);
            ChangeDescription(description);
            ChangeDuration(durationMinutes);
            ChangeAgeRestriction(ageRestriction);
            ChangePoster(posterUrl);
            ChangeTrailer(trailerUrl);
            ChangeReleaseDate(releaseDate);
            ChangeLanguage(languageId);
        }

        public void Publish()
        {
            if (Status != MovieStatus.Draft)
                throw new DomainException("Only a draft movie can be published.");

            if (_movieGenres.Count == 0)
                throw new DomainException("Movie must have at least one genre before publishing.");

            Status = MovieStatus.Published;

            Raise(new MoviePublishedEvent(Id, Title));
        }

        public void Archive()
        {
            if (Status != MovieStatus.Published)
                throw new DomainException("Only a published movie can be archived.");

            Status = MovieStatus.Archived;

            Raise(new MovieArchivedEvent(Id));
        }

        public void ChangeTitle(string title)
        {
            EnsureCanBeModified();

            ValidateTitle(title);

            string nornalizedTitle = title.Trim();

            if (Title == nornalizedTitle)
                return;

            string oldTitle = Title;

            Title = nornalizedTitle;

            Raise(new MovieTitleChangedEvent(Id, oldTitle, nornalizedTitle));
        }

        public void ChangeDescription(string description)
        {
            EnsureCanBeModified();

            ValidateDescription(description);

            string nornalizedDescription = description.Trim();

            if (Description == nornalizedDescription) 
                return;

            Description = nornalizedDescription;

            Raise(new MovieDescriptionChangedEvent(Id, nornalizedDescription));
        }

        public void ChangePoster(string poster)
        {
            EnsureCanBeModified();

            ValidatePoster(poster);

            string nornalizedPoster = poster.Trim();

            if (PosterUrl == nornalizedPoster)
                return;

            PosterUrl = nornalizedPoster;

            Raise(new MoviePosterChangedEvent(Id, nornalizedPoster));
        }

        public void ChangeTrailer(string? trailerUrl)
        {
            EnsureCanBeModified();

            ValidateTrailer(trailerUrl);

            string? normalizedTrailer = string.IsNullOrWhiteSpace(trailerUrl)
                ? null
                : trailerUrl.Trim();

            if (TrailerUrl == normalizedTrailer)
                return;

            TrailerUrl = normalizedTrailer;

            Raise(new MovieTrailerChangedEvent(Id, normalizedTrailer));
        }

        public void Restore()
        {
            if (!IsDeleted)
                return;

            MarkRestored();

            Raise(new MovieRestoredEvent(Id));
        }

        public void ChangeDuration(int durationMinutes)
        {
            EnsureCanBeModified();

            ValidateDuration(durationMinutes);

            if (DurationMinutes == durationMinutes)
                return;

            DurationMinutes = durationMinutes;

            Raise(new MovieDurationChangedEvent(Id, durationMinutes));
        }

        public void ChangeAgeRestriction(byte ageRestriction)
        {
            EnsureCanBeModified();

            ValidateAgeRestriction(ageRestriction);

            if (AgeRestriction == ageRestriction)
                return;

            AgeRestriction = ageRestriction;

            Raise(new MovieAgeRestrictionChangedEvent(Id, ageRestriction));
        }

        public void ChangeReleaseDate(DateOnly releaseDate)
        {
            EnsureCanBeModified();

            ValidateReleaseDate(releaseDate);

            if (ReleaseDate == releaseDate)
                return;

            ReleaseDate = releaseDate;

            Raise(new MovieReleaseDateChangedEvent(Id, releaseDate));
        }

        public void ChangeLanguage(Guid languageId)
        {
            EnsureCanBeModified();

            ValidateLanguage(languageId);

            if (LanguageId == languageId)
                return;

            LanguageId = languageId;

            Raise(new MovieLanguageChangedEvent(Id, languageId));
        }

        public void Delete()
        {
            if (IsDeleted)
                return;

            MarkDeleted();

            Raise(new MovieDeletedEvent(Id));
        }

        private void EnsureCanBeModified()
        {
            if (Status == MovieStatus.Archived)
            {
                throw new DomainException(
                    "Archived movie cannot be modified.");
            }

            if (IsDeleted)
            {
                throw new DomainException(
                    "Deleted movie cannot be modified.");
            }
        }
    }
}
