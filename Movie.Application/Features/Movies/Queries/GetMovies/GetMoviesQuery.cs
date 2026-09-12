using Movie.Application.Caching;
using Movie.Application.Common.Interfaces;
using Movie.Domain.Enums;
using Shared.Common.Models;

namespace Movie.Application.Features.Movies.Queries.GetMovies
{
    public sealed record GetMoviesQuery(
        string? Search,
        MovieStatus? Status,
        Guid? GenreId,
        Guid? LanguageId,
        bool IncludeDeleted,
        int Page,
        int PageSize)
        : ICachedQuery<PagedResult<GetMoviesResponse>>
    {
        public string CacheKey => CacheKeys.Movies(
            Search,
            Status,
            GenreId,
            LanguageId,
            IncludeDeleted,
            Page,
            PageSize);

        public TimeSpan? Expiration => TimeSpan.FromMinutes(5);

        public IEnumerable<string> Tags =>
        [
            CacheTags.Movies
        ];
    }
}
