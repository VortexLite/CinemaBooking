using Movie.Domain.Entities.Session;
using Microsoft.EntityFrameworkCore;
using Movie.Domain.Enums;

namespace Movie.Infrastructure.Persistence.Seed.Halls
{
    public static class SessionSeeder
    {
        public static async Task SeedAsync(MovieDbContext context,
            CancellationToken cancellationToken = default)
        {
            if (await context.Sessions.AnyAsync(cancellationToken))
                return;

            var hall = await context.Halls.FirstAsync(cancellationToken);

            var movies = await context.Movies.ToListAsync(cancellationToken);

            var currentStart = DateTime.UtcNow.Date.AddDays(1).AddHours(10);

            var sessions = new List<Session>();

            foreach (var movie in movies)
            {
                var endTime = currentStart.AddMinutes(movie.DurationMinutes);

                sessions.Add(
                    new Session(
                        movie.Id,
                        hall.Id,
                        currentStart,
                        endTime,
                        250m,
                        ProjectionType.TwoD,
                        SubtitleType.None,
                        SoundFormat.DolbyDigital));

                currentStart = endTime.AddMinutes(15);
            }

            await context.Sessions.AddRangeAsync(sessions, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
