using Movie.Domain.Common;

namespace Movie.Domain.Events.Movies
{
    public sealed class MovieGenreRemovedEvent : DomainEvent
    {
        public Guid MovieId { get; }
        public Guid GenreId { get; }

        public MovieGenreRemovedEvent(Guid movieId, Guid genreId)
        {
            MovieId = movieId;
            GenreId = genreId;
        }
    }
}
