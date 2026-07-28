using Microsoft.EntityFrameworkCore;
using Movie.Domain.Entities.Language;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(MovieDbContext context)
        {
            await context.Database.MigrateAsync();

            await GenreSeeder.SeedAsync(context);

            await HallSeeder.SeedAsync(context);

            await MovieSeeder.SeedAsync(context);

            await LanguageSeeder.SeedAsync(context);

            await context.SaveChangesAsync();
        }
    }
}
