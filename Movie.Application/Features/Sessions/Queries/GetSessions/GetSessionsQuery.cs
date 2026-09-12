using Movie.Application.Caching;
using Movie.Application.Common.Interfaces;
using Movie.Domain.Enums;

namespace Movie.Application.Features.Sessions.Queries.GetSessions
{
    public sealed record GetSessionsQuery(
        DateTime? FromUtc = null,
        DateTime? ToUtc = null,
        Guid? MovieId = null,
        Guid? HallId = null,
        SessionStatus? Status = null,
        bool IncludeDeleted = false)
        : ICachedQuery<List<GetSessionsResponse>>
    {
        public string CacheKey => CacheKeys.Sessions(
            FromUtc,
            ToUtc,
            MovieId,
            HallId,
            Status,
            IncludeDeleted);

        public TimeSpan? Expiration => TimeSpan.FromMinutes(30);

        public IEnumerable<string>? Tags =>
        [
            CacheTags.Sessions
        ];
    }
}
