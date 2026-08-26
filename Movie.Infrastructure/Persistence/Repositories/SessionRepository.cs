using Microsoft.EntityFrameworkCore;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Entities.Hall;
using Movie.Domain.Entities.Session;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Repositories
{
    public sealed class SessionRepository
        : BaseRepository<Session>,
        ISessionRepository
    {
        public SessionRepository(MovieDbContext context)
            : base(context) { }

        public async Task<Session?> GetDetailsAsync(
            Guid id, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Sessions
                .Include(x => x.Movie)
                    .ThenInclude(x => x.Language)
                .Include(x => x.Movie)
                    .ThenInclude(x => x.MovieGenres)
                        .ThenInclude(x => x.Genre)
                .Include(x => x.Hall)
                .FirstOrDefaultAsync(x => x.Id == id,
                cancellationToken);
        }

        public async Task<List<Session>> GetMovieSessionsAsync(
            Guid movieId, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Sessions
                .AsNoTracking()
                .Include(x => x.Hall)
                .Where(x => x.MovieId == movieId)
                .OrderBy(x => x.StartTime)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Session>> GetHallSessionsAsync(
            Guid hallId, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Sessions
                .AsNoTracking()
                .Include(x => x.Movie)
                .Where(x => x.HallId == hallId)
                .OrderBy(x => x.StartTime)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Session>> GetUpcomingAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Sessions
                .AsNoTracking()
                .Include(x => x.Movie)
                .Include(x => x.Hall)
                .Where(x => x.StartTime >= DateTime.UtcNow)
                .OrderBy(x => x.StartTime)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Session>> GetByDateAsync(
            DateOnly date, 
            CancellationToken cancellationToken = default)
        {
            var start = date.ToDateTime(TimeOnly.MinValue);
            var end = start.AddDays(1);

            return await _context.Sessions
                .AsNoTracking()
                .Include(x => x.Movie)
                .Include(x => x.Hall)
                .Where(x =>
                    x.StartTime >= start &&
                    x.StartTime < end)
                .OrderBy(x => x.StartTime)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsHallBusyAsync(
            Guid hallId, 
            DateTime start, 
            DateTime end, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Sessions
                .AnyAsync(x =>
                    x.HallId == hallId &&
                    start < x.EndTime &&
                    end > x.StartTime,
                    cancellationToken);
        }

        public async Task<Session?> GetForBookingAsync(
            Guid sessionId,
            CancellationToken cancellationToken = default)
        {
            return await _context.Sessions
                .AsNoTracking()
                .Include(x => x.Movie)
                .Include(x => x.Hall)
                    .ThenInclude(h => h.Seats)
                .FirstOrDefaultAsync(
                    x => x.Id == sessionId,
                    cancellationToken);
        }
    }
}
