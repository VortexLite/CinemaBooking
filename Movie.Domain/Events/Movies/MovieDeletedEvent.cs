using Movie.Domain.Common;

namespace Movie.Domain.Events.Movies
{
    public sealed class MovieDeletedEvent : DomainEvent
    {
        public Guid MovieId { get; }

        public MovieDeletedEvent(Guid movieId)
        {
            MovieId = movieId;
        }
    }
}
