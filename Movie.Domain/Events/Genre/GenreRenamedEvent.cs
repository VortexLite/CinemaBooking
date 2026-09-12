using Movie.Domain.Common;

namespace Movie.Domain.Events.Genre
{
    public sealed class GenreRenamedEvent : DomainEvent
    {
        public Guid GenreId { get; }

        public string Name { get; }

        public GenreRenamedEvent(
            Guid genreId,
            string name)
        {
            GenreId = genreId;
            Name = name;
        }
    }
}
