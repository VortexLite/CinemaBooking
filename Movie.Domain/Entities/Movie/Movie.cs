using Movie.Domain.Common;
using Movie.Domain.Entities.Language;
using Movie.Domain.Enums;
using Movie.Domain.Events.Movie;
using Movie.Domain.Extensions;

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
        public ICollection<MovieGenre.MovieGenre> MovieGenres { get; }
            = new List<MovieGenre.MovieGenre>();
        public ICollection<Session.Session> Sessions { get; }
            = new List<Session.Session>();

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
            ValidateDuration(durationMinutes);

            Title = title.Trim();
            Description = description.Trim();
            DurationMinutes = durationMinutes;
            AgeRestriction = ageRestriction;
            PosterUrl = posterUrl;
            TrailerUrl = trailerUrl;
            ReleaseDate = releaseDate;

            LanguageId = languageId;
            Status = MovieStatus.Draft;

            Raise(new MovieCreatedEvent(Id));
        }
    }
}
