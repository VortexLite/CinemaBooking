using Movie.Domain.Common;

namespace Movie.Domain.Entities.MovieGenre
{
    public sealed class MovieGenre : AggregateRoot
    {
        public Guid MovieId { get; private set; }
        public Movie.Movie Movie { get; private set; } = null!;
        public Guid GenreId { get; private set; }
        public Genre.Genre Genre { get; private set; } = null!;

        private MovieGenre() { }
        public MovieGenre(
            Guid movieId,
            Guid genreId)
        {
            MovieId = movieId;
            GenreId = genreId;
        }
    }
}
