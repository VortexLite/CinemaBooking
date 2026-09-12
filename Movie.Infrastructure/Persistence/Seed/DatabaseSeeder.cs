using Movie.Infrastructure.Persistence.Seed.Genres;
using Movie.Infrastructure.Persistence.Seed.Halls;
using Movie.Infrastructure.Persistence.Seed.Languages;
using Movie.Infrastructure.Persistence.Seed.Movies;

namespace Movie.Infrastructure.Persistence.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(MovieDbContext context)
        {
            await LanguageSeeder.SeedAsync(context);

            await GenreSeeder.SeedAsync(context);

            await MovieSeeder.SeedAsync(context);

            await MovieGenreSeeder.SeedAsync(context);

            await HallSeeder.SeedAsync(context);

            await SessionSeeder.SeedAsync(context);
        }
    }
}
