using Movie.Domain.Common;
using Movie.Domain.Extensions;

namespace Movie.Domain.Entities.Genre
{
    public sealed class Genre : AggregateRoot
    {
        public string Name { get; private set; } = string.Empty;
        public ICollection<MovieGenre.MovieGenre> MovieGenres { get; } 
            = new List<MovieGenre.MovieGenre>();

        private Genre() { }

        public Genre(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new DomainException("Genre name is required");

            Name = name.Trim();
        }
    }
}
