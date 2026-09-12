using Movie.Domain.Common;
using Movie.Domain.Enums;
using Movie.Domain.Events.Movies;

namespace Movie.Domain.Entities.Movie
{
    public sealed partial class Movie : AggregateRoot
    {
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public int DurationMinutes { get; private set; }
        public byte AgeRestriction { get; private set; }
        public string PosterUrl { get; private set; } = string.Empty;
        public string? TrailerUrl { get; private set; }
        public DateOnly ReleaseDate { get; private set; }
        public MovieStatus Status { get; private set; }
        public Guid LanguageId { get; private set; }
        public Language.Language Language { get; private set; } = null!;

        private readonly List<MovieGenre.MovieGenre> _movieGenres = [];
        public IReadOnlyCollection<MovieGenre.MovieGenre> MovieGenres
            => _movieGenres;

        private readonly List<Session.Session> _sessions = [];
        public IReadOnlyCollection<Session.Session> Sessions
            => _sessions;

        private Movie() { }

        public Movie(
            string title,
            string description,
            int durationMinutes,
            byte ageRestriction,
            string posterUrl,
            string? trailerUrl,
            DateOnly releaseDate,
            Guid languageId)
        {
            ValidateTitle(title);
            ValidateDescription(description);
            ValidateDuration(durationMinutes);
            ValidateAgeRestriction(ageRestriction);
            ValidatePoster(posterUrl);
            ValidateTrailer(trailerUrl);
            ValidateReleaseDate(releaseDate);
            ValidateLanguage(languageId);

            Title = title.Trim();
            Description = description.Trim();
            DurationMinutes = durationMinutes;
            AgeRestriction = ageRestriction;
            PosterUrl = posterUrl.Trim();
            TrailerUrl = string.IsNullOrWhiteSpace(trailerUrl)
                ? null
                : trailerUrl.Trim();
            ReleaseDate = releaseDate;
            LanguageId = languageId;

            Status = MovieStatus.Draft;

            Raise(new MovieCreatedEvent(Id));
        }
    }
}
