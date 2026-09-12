using Microsoft.EntityFrameworkCore;
using Movie.Domain.Entities.Language;

namespace Movie.Infrastructure.Persistence.Seed.Languages
{
    public static class LanguageSeeder
    {
        public static async Task SeedAsync(MovieDbContext context,
            CancellationToken cancellationToken = default)
        {
            if (await context.Languages.AnyAsync(cancellationToken))
                return;

            var languages = new[]
            {
                new Language(
                    "English",
                    "English",
                    "EN"),

                new Language(
                    "Ukrainian",
                    "Українська",
                    "UK"),

                new Language(
                    "Polish",
                    "Polski",
                    "PL"),

                new Language(
                    "French",
                    "Français",
                    "FR"),

                new Language(
                    "German",
                    "Deutsch",
                    "DE")
            };

            await context.Languages.AddRangeAsync(languages, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
