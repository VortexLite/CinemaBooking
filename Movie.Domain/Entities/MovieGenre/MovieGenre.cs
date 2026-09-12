namespace Movie.Domain.Entities.MovieGenre
{
    public sealed class MovieGenre
    {
        public Guid MovieId { get; private set; }
        public Movie.Movie Movie { get; private set; } = null!;
        public Guid GenreId { get; private set; }
        public Genre.Genre Genre { get; private set; } = null!;

        private MovieGenre() { }
        internal MovieGenre(
            Guid movieId,
            Guid genreId)
        {
            if (movieId == Guid.Empty)
                throw new ArgumentException("Movie id is required", nameof(genreId));

            if (genreId == Guid.Empty)
                throw new ArgumentException("Genre id is required.", nameof(genreId));

            MovieId = movieId;
            GenreId = genreId;
        }
    }
}
