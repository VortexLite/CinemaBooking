using Microsoft.EntityFrameworkCore;
using Movie.Domain.Entities.Hall;
using Movie.Domain.Entities.Language;
using System;
using System.Collections.Generic;
using System.Text;


namespace Movie.Infrastructure.Persistence.Seed.Halls
{
    public static class HallSeeder
    {
        public static async Task SeedAsync(MovieDbContext context)
        {
            if (await context.Halls.AnyAsync())
                return;

            await context.Halls.AddRangeAsync(
                new Hall("Hall 1", 10, 16),
                new Hall("Hall 2", 12, 20),
                new Hall("VIP Hall", 8, 10)
            );

            await context.SaveChangesAsync();
        }
    }
}
