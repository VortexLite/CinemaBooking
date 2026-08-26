using Movie.Domain.Entities.Session;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Movie.Infrastructure.Persistence.Seed.Halls
{
    public static class SessionSeeder
    {
        public static async Task SeedAsync(MovieDbContext context)
        {
            if (await context.Sessions.AnyAsync())
                return;

            var hall = await context.Halls.FirstAsync();

            var movies = await context.Movies.ToListAsync();

            foreach (var movie in movies)
            {
                context.Sessions.Add(
                    new Session(
                        movie.Id,
                        hall.Id,
                        DateTime.UtcNow.AddDays(1).AddHours(18),
                        DateTime.UtcNow.AddDays(1).AddHours(21),
                        250));
            }

            await context.SaveChangesAsync();
        }
    }
}
