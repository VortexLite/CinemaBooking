using Movie.Domain.Common;

namespace Movie.Domain.Events.Genre
{
    public sealed class GenreCreatedEvent : DomainEvent
    {
        public Guid GenreId { get; }

        public GenreCreatedEvent(Guid genreId)
        {
            GenreId = genreId;
        }
    }
}
