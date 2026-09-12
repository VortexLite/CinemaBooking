using Microsoft.EntityFrameworkCore;

namespace Movie.Infrastructure.Persistence.Seed.Movies
{
    public static class MovieGenreSeeder
    {
        public static async Task SeedAsync(MovieDbContext context)
        {
            if (await context.MovieGenres.AnyAsync())
                return;

            var genres = await context.Genres
                .ToDictionaryAsync(x => x.Name);

            var movies = await context.Movies
                .ToDictionaryAsync(x => x.Title);

            movies["Avatar: The Way of Water"].AddGenre(genres["Action"]);
            movies["Avatar: The Way of Water"].AddGenre(genres["Adventure"]);
            movies["Avatar: The Way of Water"].AddGenre(genres["Fantasy"]);

            movies["Oppenheimer"].AddGenre(genres["Drama"]);
            movies["Oppenheimer"].AddGenre(genres["History"]);
            movies["Oppenheimer"].AddGenre(genres["Biography"]);

            movies["Mavka: The Forest Song"].AddGenre(genres["Animation"]);
            movies["Mavka: The Forest Song"].AddGenre(genres["Fantasy"]);
            movies["Mavka: The Forest Song"].AddGenre(genres["Family"]);

            await context.SaveChangesAsync();
        }
    }
}
