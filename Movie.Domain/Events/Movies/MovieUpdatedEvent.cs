using Movie.Domain.Common;

namespace Movie.Domain.Events.Movies
{
    public sealed class MovieUpdatedEvent : DomainEvent
    {
        public Guid MovieId { get; }

        public MovieUpdatedEvent(Guid movieId)
        {
            MovieId = movieId;
        }
    }
}
