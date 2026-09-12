using Movie.Domain.Common;

namespace Movie.Domain.Events.Movies
{
    public sealed class MoviePosterChangedEvent : DomainEvent
    {
        public Guid MovieId { get; }
        public string PosterUrl { get; }

        public MoviePosterChangedEvent(Guid movieId, string posterUrl)
        {
            MovieId = movieId;
            PosterUrl = posterUrl;
        }
    }
}
