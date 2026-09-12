using Movie.Application.Caching;
using Movie.Application.Common.Interfaces;

namespace Movie.Application.Features.Movies.Queries.GetMovie
{
    public sealed record GetMovieQuery(Guid Id)
        : ICachedQuery<GetMovieResponse>
    {
        public string CacheKey => CacheKeys.Movie(Id);

        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);

        public IEnumerable<string> Tags =>
        [
            CacheTags.Movies,
            CacheTags.Movie(Id)
        ];
    }
    
}
