using Movie.Domain.Enums;
using Movie.Domain.Events.Movies;
using Movie.Domain.Exceptions;

namespace Movie.Domain.Entities.Movie
{
    public sealed partial class Movie
    {
        public void AddGenre(Genre.Genre genre)
        {
            EnsureCanBeModified();

            ArgumentNullException.ThrowIfNull(genre);

            if (_movieGenres.Any(x => x.GenreId == genre.Id))
                return;

            _movieGenres.Add(new MovieGenre.MovieGenre(Id, genre.Id));

            Raise(new MovieGenreAddedEvent(Id, genre.Id));
        }

        public void RemoveGenre(Guid genreId)
        {
            EnsureCanBeModified();

            if (genreId == Guid.Empty)
                throw new DomainException("Genre id is required");

            var movieGenre = _movieGenres.FirstOrDefault(x => x.GenreId == genreId);

            if (movieGenre is null)
                return;

            if (Status == MovieStatus.Published &&
                _movieGenres.Count == 1)
                throw new DomainException("Published movie must have at least one genre.");

            _movieGenres.Remove(movieGenre);

            Raise(new MovieGenreRemovedEvent(Id, genreId));
        }
    }
}
