using Movie.Domain.Common;

namespace Movie.Domain.Events.Genre
{
    public sealed class GenreRestoredEvent : DomainEvent
    {
        public Guid GenreId { get; }

        public GenreRestoredEvent(Guid genreId)
        {
            GenreId = genreId;
        }
    }
}
