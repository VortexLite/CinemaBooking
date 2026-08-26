using Movie.Domain.Entities.MovieGenre;
using Movie.Domain.Events.Movies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Entities.Movie
{
    public sealed partial class Movie
    {
        public void AddGenre(Genre.Genre genre)
        {
            ArgumentNullException.ThrowIfNull(genre);

            if (_movieGenres.Any(x => x.GenreId == genre.Id))
                return;

            _movieGenres.Add(new MovieGenre.MovieGenre(Id, genre.Id));

            Raise(new MovieGenreAddedEvent(Id, genre.Id));
        }

        public void RemoveGenre(Guid genreId)
        {
            var movieGenre =
                _movieGenres.FirstOrDefault(x => x.GenreId == genreId);

            if (movieGenre is null)
                return;

            _movieGenres.Remove(movieGenre);

            Raise(new MovieGenreRemovedEvent(Id, movieGenre.Id));
        }
    }
}
