using Microsoft.EntityFrameworkCore;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Entities.Session;
using Movie.Domain.Enums;

namespace Movie.Infrastructure.Persistence.Repositories
{
    public sealed class SessionRepository
        : BaseRepository<Session>, ISessionRepository
    {
        public SessionRepository(MovieDbContext context)
            : base(context) { }

        public async Task<Session?> GetByIdIncludingDeletedAsync(Guid id, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Sessions
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == id,
                cancellationToken);
        }

        public async Task<bool> HasConflictAsync(Guid hallId, DateTime startTime, 
            DateTime endTime, 
            TimeSpan buffer, 
            Guid? excludeSessionId = null, 
            CancellationToken cancellationToken = default)
        {
            var startWithBuffer = startTime - buffer;

            var endWithBuffer = endTime + buffer;

            return await _context.Sessions
                .AnyAsync(x => 
                    x.HallId == hallId &&
                    (!excludeSessionId.HasValue || 
                    x.Id != excludeSessionId.Value) &&
                    x.StartTime < endWithBuffer &&
                    x.EndTime > startWithBuffer, 
                    cancellationToken);
        }

        public async Task<List<Session>> GetSessionToStartAsync(DateTime utcNow,
            int batchSize, CancellationToken cancellationToken = default)
        {
            return await _context.Sessions
                .Where(x =>
                    !x.IsDeleted &&
                    x.StartTime <= utcNow &&
                    (
                        x.Status == SessionStatus.Scheduled ||
                        x.Status == SessionStatus.OpenForBooking
                    ))
                .OrderBy(x => x.StartTime)
                .Take(batchSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Session>> GetSessionToFinishAsync(DateTime utcNow,
            int batchSize, CancellationToken cancellationToken = default)
        {
            return await _context.Sessions
                .Where(x =>
                    !x.IsDeleted &&
                    x.EndTime <= utcNow &&
                    x.Status == SessionStatus.Started)
                .OrderBy(x => x.EndTime)
                .Take(batchSize)
                .ToListAsync(cancellationToken);
        }
    }
}
