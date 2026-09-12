using Movie.Domain.Common;

namespace Movie.Domain.Events.Movies
{
    public sealed class MovieGenreAddedEvent : DomainEvent
    {
        public Guid MovieId { get; }
        public Guid GenreId { get; }
        
        public MovieGenreAddedEvent(Guid movieId, Guid genreId)
        {
            MovieId = movieId;
            GenreId = genreId;
        }
    }
}
