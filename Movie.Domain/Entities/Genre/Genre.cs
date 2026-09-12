using Movie.Domain.Common;
using Movie.Domain.Events.Genre;

namespace Movie.Domain.Entities.Genre
{
    public sealed partial class Genre : AggregateRoot
    {
        public string Name { get; private set; } = string.Empty;

        private readonly List<MovieGenre.MovieGenre> _movieGenres = [];
        public IReadOnlyCollection<MovieGenre.MovieGenre> MovieGenres
            => _movieGenres;

        private Genre() { }

        public Genre(string name)
        {
            ValidateName(name);

            Name = name.Trim();

            Raise(new GenreCreatedEvent(Id));
        }
    }
}
