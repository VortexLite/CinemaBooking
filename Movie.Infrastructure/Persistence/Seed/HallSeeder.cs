using Microsoft.EntityFrameworkCore;
using Movie.Domain.Entities.Hall;
using Movie.Domain.Entities.Language;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Seed
{
    public static class HallSeeder
    {
        public static async Task SeedAsync(MovieDbContext context)
        {
            if (await context.Hall.AnyAsync())
                return;

            await context.Hall.AddRangeAsync(
                new Hall("Hall 1", 10, 15),
                new Hall("Hall 2", 12, 18),
                new Hall("VIP Hall", 6, 10)
            );
        }
    }
}
