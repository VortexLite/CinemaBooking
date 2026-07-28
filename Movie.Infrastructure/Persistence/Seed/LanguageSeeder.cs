using Microsoft.EntityFrameworkCore;
using Movie.Domain.Entities.Language;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Seed
{
    public static class LanguageSeeder
    {
        public static async Task SeedAsync(MovieDbContext context)
        {
            if (await context.Languages.AnyAsync())
                return;

            await context.Languages.AddRangeAsync(
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
            );
        }
    }
}
