using Movie.Domain.Common;

namespace Movie.Domain.Events.Movies
{
    public sealed class MovieArchivedEvent : DomainEvent
    {
        public Guid MovieId { get; }

        public MovieArchivedEvent(Guid movieId)
        {
            MovieId = movieId;
        }
    }
}
