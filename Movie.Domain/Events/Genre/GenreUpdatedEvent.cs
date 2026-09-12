using Movie.Domain.Common;

namespace Movie.Domain.Events.Genre
{
    public sealed class GenreUpdatedEvent : DomainEvent
    {
        public Guid GenreId { get; }

        public GenreUpdatedEvent(Guid genreId)
        {
            GenreId = genreId;
        }
    }
}
