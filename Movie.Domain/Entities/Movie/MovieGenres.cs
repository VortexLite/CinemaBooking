using Movie.Domain.Entities.MovieGenre;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Entities.Movie
{
    public sealed partial class Movie
    {
        public void AddGenre(Genre.Genre genre)
        {
            if (MovieGenres.Any(x => x.GenreId == genre.Id))
                return;

            MovieGenres.Add(new MovieGenre.MovieGenre(Id, genre.Id));
        }

        public void RemoveGenre(Guid genreId)
        {
            var movieGenre =
                MovieGenres.FirstOrDefault(x => x.GenreId == genreId);

            if (movieGenre is null)
                return;

            MovieGenres.Remove(movieGenre);
        }
    }
}
