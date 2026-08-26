using Movie.Application.Caching;
using Movie.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Genres.Queries.GetGenres
{
    public sealed record GetGenresQuery(bool IncludeDeleted = false)
        : ICachedQuery<List<GetGenresResponse>>
    {
        public string CacheKey => CacheKeys.Genres(IncludeDeleted);

        public TimeSpan? Expiration => TimeSpan.FromMinutes(12);

        public IEnumerable<string>? Tags =>
        [
            CacheTags.Genres
        ];
    }
}
