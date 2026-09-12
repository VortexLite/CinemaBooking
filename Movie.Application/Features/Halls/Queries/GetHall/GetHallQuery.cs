using Movie.Application.Caching;
using Movie.Application.Common.Interfaces;

namespace Movie.Application.Features.Halls.Queries.GetHall
{
    public sealed record GetHallQuery(Guid Id)
        : ICachedQuery<GetHallResponse>
    {
        public string CacheKey => CacheKeys.Hall(Id);

        public TimeSpan? Expiration => TimeSpan.FromHours(2);

        public IEnumerable<string>? Tags =>
        [
            CacheTags.Halls,
            CacheTags.Hall(Id)
        ];
    }
}
