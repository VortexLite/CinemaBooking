using Microsoft.EntityFrameworkCore;
using Movie.Domain.Entities.Hall;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Seed
{
    public static class MovieSeeder
    {
        public static async Task SeedAsync(MovieDbContext context)
        {
            if (await context.Movies.AnyAsync())
                return;

            var movie = Domain.Entities.Movie.Movie.Create(
                title: "The Matrix",
                description: "Neo discovers reality.",
                durationMinutes: 136,
                ageRestriction: 16,
                posterUrl: "/images/matrix.jpg",
                trailerUrl: null,
                releaseDate: new DateOnly(1999, 3, 31));

            await context.Movies.AddAsync(
                new Domain.Entities.Movie.Movie(
                    "The Matrix",
                    "Neo discovers reality.",
                    136,
                    16,
                    "/images/matrix.jpg",
                    null,
                    new DateOnly(1999, 3, 31)
                    )
            );

            await context.Movies.AddAsync(movie);
        }
    }
}
