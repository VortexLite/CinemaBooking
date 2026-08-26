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
            ChangeTitle(title);
            ChangeDescription(description);
            ChangeDuration(durationMinutes);
            ChangeAgeRestriction(ageRestriction);
            ChangePoster(posterUrl);
            ChangeTrailer(trailerUrl);
            ChangeReleaseDate(releaseDate);
            ChangeLanguage(languageId);

            Raise(new MovieUpdatedEvent(Id));
        }

        public void Publish()
        {
            if (Status == MovieStatus.Published)
                throw new DomainException("Movie already published.");

            Status = MovieStatus.Published;

            Raise(new MoviePublishedEvent(Id, Title));
        }

        public void Archive()
        {
            if (Status == MovieStatus.Archived)
                return;

            Status = MovieStatus.Archived;

            Raise(new MovieArchivedEvent(Id));
        }

        public void ChangeTitle(string title)
        {
            ValidateTitle(title);

            Raise(new MovieTitleChangedEvent(Id, Title, title));

            Title = title.Trim();
        }

        public void ChangeDescription(string description)
        {
            Description = description.Trim();

            Raise(new MovieDescriptionChangedEvent(Id, description));
        }

        public void ChangePoster(string poster)
        {
            ValidatePoster(poster);

            PosterUrl = poster;

            Raise(new MoviePosterChangedEvent(Id, poster));
        }

        public void ChangeTrailer(string? trailer)
        {
            TrailerUrl = trailer;

            Raise(new MovieTrailerChangedEvent(Id, trailer));
        }

        public void Restore()
        {
            MarkRestored();

            Raise(new MovieRestoredEvent(Id));
        }

        public void ChangeDuration(int durationMinutes)
        {
            ValidateDuration(durationMinutes);

            if (DurationMinutes == durationMinutes)
                return;

            DurationMinutes = durationMinutes;

            Raise(new MovieDurationChangedEvent(Id, durationMinutes));
        }

        public void ChangeAgeRestriction(byte ageRestriction)
        {
            ValidateAgeRestriction(ageRestriction);

            if (AgeRestriction == ageRestriction)
                return;

            AgeRestriction = ageRestriction;

            Raise(new MovieAgeRestrictionChangedEvent(Id, ageRestriction));
        }

        public void ChangeReleaseDate(DateOnly releaseDate)
        {
            ValidateReleaseDate(releaseDate);

            if (ReleaseDate == releaseDate)
                return;

            ReleaseDate = releaseDate;

            Raise(new MovieReleaseDateChangedEvent(Id, releaseDate));
        }

        public void ChangeLanguage(Guid languageId)
        {
            ValidateLanguage(languageId);

            if (LanguageId == languageId)
                return;

            LanguageId = languageId;

            Raise(new MovieLanguageChangedEvent(Id, languageId));
        }
    }
}
