using Microsoft.EntityFrameworkCore;
using Movie.Application.Common.Interfaces;
using Movie.Application.Features.Sessions.Queries.GetSession;
using Movie.Application.Features.Sessions.Queries.GetSessions;
using Movie.Domain.Enums;

namespace Movie.Infrastructure.Persistence.Queries
{
    public sealed class SessionQueries
        : BaseQuery, ISessionQueries
    {
        public SessionQueries(MovieDbContext context) : base(context) { }

        public async Task<GetSessionResponse?> GetSessionAsync(Guid Id, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Sessions
                .AsNoTracking()
                .Where(x => x.Id == Id)
                .Select(x => new GetSessionResponse
                {
                    Id = x.Id,
                    MovieId = x.MovieId,
                    MovieTitle = _context.Movies
                        .Where(m => m.Id == x.MovieId)
                        .Select(m => m.Title)
                        .First(),
                    HallId = x.HallId,
                    HallName = _context.Halls
                        .Where(h => h.Id == x.HallId)
                        .Select(h => h.Name)
                        .First(),
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    Price = x.Price,
                    SeatsCount = _context.Seats
                        .Where(s => s.HallId == x.HallId)
                        .Count(),
                    Status = x.Status,
                    ProjectionType = x.ProjectionType,
                    SubtitleType = x.SubtitleType,
                    SoundFormat = x.SoundFormat
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task<List<GetSessionsResponse>> GetSessionsAsync(DateTime? fromUtc, 
            DateTime? toUtc, 
            Guid? movieId, 
            Guid? hallId,
            SessionStatus? status,
            bool includeDeleted,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Sessions.AsNoTracking();

            if (includeDeleted)
                query = query.IgnoreQueryFilters();

            if (fromUtc.HasValue)
                query = query.Where(x => x.StartTime >= fromUtc.Value);

            if (toUtc.HasValue)
                query = query.Where(x => x.StartTime < toUtc.Value);

            if (movieId.HasValue)
                query = query.Where(x => x.MovieId == movieId.Value);

            if (hallId.HasValue)
                query = query.Where(x => x.HallId == hallId.Value);

            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);

            return query
                .OrderBy(x => x.StartTime)
                .Select(x => new GetSessionsResponse
                {
                    Id = x.Id,
                    MovieId = x.MovieId,
                    MovieTitle = _context.Movies
                        .Where(m => m.Id == x.MovieId)
                        .Select(m => m.Title)
                        .First(),
                    HallId = x.HallId,
                    HallName = _context.Halls
                        .Where(h => h.Id == x.HallId)
                        .Select(h => h.Name)
                        .First(),
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    Price = x.Price,
                    Status = x.Status,
                    ProjectionType = x.ProjectionType,
                    SubtitleType = x.SubtitleType,
                    SoundFormat = x.SoundFormat
                })
                .ToListAsync(cancellationToken);
        }
    }
}
