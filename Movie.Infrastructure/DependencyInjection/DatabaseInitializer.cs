using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movie.Infrastructure.Persistence;
using Movie.Infrastructure.Persistence.Seed;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.DependencyInjection
{
    public static class DatabaseInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            await using (var scope = serviceProvider.CreateAsyncScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();

                await dbContext.Database.MigrateAsync();

                await DatabaseSeeder.SeedAsync(dbContext);
            }
        }
    }
}
