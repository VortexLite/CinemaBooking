using Movie.Domain.Common;

namespace Movie.Domain.Events.Genre
{
    public sealed class GenreDeletedEvent : DomainEvent
    {
        public Guid GenreId { get; }

        public GenreDeletedEvent(Guid genreId)
        {
            GenreId = genreId;
        }
    }
}
