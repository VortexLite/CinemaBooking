using Movie.Domain.Common;

namespace Movie.Domain.Events.Movies
{
    public sealed class MoviePublishedEvent : DomainEvent
    {
        public Guid MovieId { get; }
        public string Title { get; }

        public MoviePublishedEvent(Guid movieId, string title)
        {
            MovieId = movieId;
            Title = title;
        }
    }
}
