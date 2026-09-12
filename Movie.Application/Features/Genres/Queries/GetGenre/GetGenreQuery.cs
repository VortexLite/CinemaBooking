using Movie.Application.Caching;
using Movie.Application.Common.Interfaces;

namespace Movie.Application.Features.Genres.Queries.GetGenre
{
    public sealed record GetGenreQuery(Guid Id)
        : ICachedQuery<GetGenreResponse>
    {
        public string CacheKey => CacheKeys.Genre(Id);

        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);

        public IEnumerable<string>? Tags => 
        [
           CacheTags.Genres,
           CacheTags.Genre(Id)
        ];
    }
}
