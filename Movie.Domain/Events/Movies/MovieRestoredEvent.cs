using Movie.Domain.Common;

namespace Movie.Domain.Events.Movies
{
    public sealed class MovieRestoredEvent : DomainEvent
    {
        public Guid MovieId { get; }

        public MovieRestoredEvent(Guid movieId)
        {
            MovieId = movieId;
        }
    }
}
