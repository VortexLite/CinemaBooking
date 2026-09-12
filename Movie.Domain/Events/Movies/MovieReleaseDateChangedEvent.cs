using Movie.Domain.Common;

namespace Movie.Domain.Events.Movies
{
    public sealed class MovieReleaseDateChangedEvent : DomainEvent
    {
        public Guid MovieId { get; }
        public DateOnly ReleaseDate { get; }

        public MovieReleaseDateChangedEvent(
            Guid movieId,
            DateOnly releaseDate)
        {
            MovieId = movieId;
            ReleaseDate = releaseDate;
        }
    }
}
