using Microsoft.EntityFrameworkCore;
using Movie.Domain.Entities.Genre;
using Movie.Domain.Entities.Language;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Seed.Genres
{
    public static class GenreSeeder
    {
        public static async Task SeedAsync(MovieDbContext context,
            CancellationToken cancellationToken = default)
        {
            if (await context.Genres.AnyAsync(cancellationToken))
                return;

            var genres = new[]
            {
                new Genre("Action"),
                new Genre("Adventure"),
                new Genre("Comedy"),
                new Genre("Crime"),
                new Genre("Drama"),
                new Genre("Fantasy"),
                new Genre("Horror"),
                new Genre("Romance"),
                new Genre("Sci-Fi"),
                new Genre("Thriller"),
                new Genre("Animation"),
                new Genre("Documentary"),
                new Genre("History"),
                new Genre("Biography"),
                new Genre("Family")
            };

            await context.AddRangeAsync(genres, cancellationToken);

            await context.SaveChangesAsync();
        }
    }
}
