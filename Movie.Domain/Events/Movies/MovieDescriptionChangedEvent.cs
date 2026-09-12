using Movie.Domain.Common;

namespace Movie.Domain.Events.Movies
{
    public sealed class MovieDescriptionChangedEvent : DomainEvent
    {
        public Guid MovieId { get; }
        public string Description { get; }

        public MovieDescriptionChangedEvent(
            Guid movieId,
            string description)
        {
            MovieId = movieId;
            Description = description;
        }
    }
}
