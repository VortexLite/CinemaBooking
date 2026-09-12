using Microsoft.EntityFrameworkCore;
using Movie.Domain.Common;
using Movie.Domain.Entities.Genre;
using Movie.Domain.Entities.Hall;
using Movie.Domain.Entities.Language;
using Movie.Domain.Entities.MovieGenre;
using Movie.Domain.Entities.Seat;
using Movie.Domain.Entities.Session;
using Movie.Infrastructure.Persistence.Extensions;
using Movie.Infrastructure.Persistence.Outbox;

namespace Movie.Infrastructure.Persistence
{
    public sealed class MovieDbContext : DbContext, IUnitOfWork
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.Entities.Movie.Movie> Movies => Set<Domain.Entities.Movie.Movie>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<MovieGenre> MovieGenres => Set<MovieGenre>();
        public DbSet<Hall> Halls => Set<Hall>();
        public DbSet<Seat> Seats => Set<Seat>();
        public DbSet<Session> Sessions => Set<Session>();
        public DbSet<Language> Languages => Set<Language>();
        public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
        public DbSet<OutboxDeadLetter> OutboxDeadLetters => Set<OutboxDeadLetter>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("movie");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieDbContext).Assembly);

            modelBuilder.ApplySoftDeleteQueryFilter();

            base.OnModelCreating(modelBuilder);
        }
    }
}
