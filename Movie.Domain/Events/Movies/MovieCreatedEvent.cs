using Movie.Domain.Common;

namespace Movie.Domain.Events.Movies
{
    public sealed class MovieCreatedEvent : DomainEvent
    {
        public Guid MovieId { get; }

        public MovieCreatedEvent(Guid movieId)
        {
            MovieId = movieId;
        }
    }
}
