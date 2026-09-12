using Movie.Domain.Common;

namespace Movie.Domain.Events.Movies
{
    public sealed class MovieTrailerChangedEvent : DomainEvent
    {
        public Guid MovieId { get; }
        public string? TrailerUrl { get; }

        public MovieTrailerChangedEvent(
            Guid movieId,
            string? trailerUrl)
        {
            MovieId = movieId;
            TrailerUrl = trailerUrl;
        }
    }
}
