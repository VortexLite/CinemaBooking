using Microsoft.EntityFrameworkCore;
using Movie.Domain.Entities.Movie;


namespace Movie.Infrastructure.Persistence.Seed.Movies
{
    public static class MovieSeeder
    {
        public static async Task SeedAsync(MovieDbContext context,
            CancellationToken cancellationToken = default)
        {
            if (await context.Movies.AnyAsync(cancellationToken))
                return;

            var english = await context.Languages
                .FirstAsync(x => x.IsoCode == "EN",
                cancellationToken);

            var ukrainian = await context.Languages
                .FirstAsync(x => x.IsoCode == "UK",
                cancellationToken);

            var movies = new[]
            {
            new Domain.Entities.Movie.Movie(
                "Avatar: The Way of Water",
                "Jake Sully lives with his family on Pandora.",
                192,
                12,
                "https://image.tmdb.org/t/p/original/t6HIqrRAclMCA60NsSmeqe9RmNV.jpg",
                "https://www.youtube.com/watch?v=d9MyW72ELq0",
                new DateOnly(2022,12,16),
                english.Id),

            new Domain.Entities.Movie.Movie(
                "Oppenheimer",
                "Biography of J. Robert Oppenheimer.",
                180,
                16,
                "https://image.tmdb.org/t/p/original/ptpr0kGAckfQkJeJIt8st5dglvd.jpg",
                "https://www.youtube.com/watch?v=uYPbbksJxIg",
                new DateOnly(2023,7,21),
                english.Id),

            new Domain.Entities.Movie.Movie(
                "Mavka: The Forest Song",
                "Animated fantasy based on Ukrainian folklore.",
                99,
                0,
                "https://upload.wikimedia.org/wikipedia/en/3/3c/Mavka_film_poster.jpg",
                "https://www.youtube.com/watch?v=K4DyBUG242c",
                new DateOnly(2023,3,2),
                ukrainian.Id)
        };

            foreach (var movie in movies)
                movie.Publish();

            await context.Movies.AddRangeAsync(movies, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
