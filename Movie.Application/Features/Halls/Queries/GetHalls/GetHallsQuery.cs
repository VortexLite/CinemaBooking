using Movie.Application.Caching;
using Movie.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Halls.Queries.GetHalls
{
    public sealed record GetHallsQuery(bool IncludeDeleted = false)
        : ICachedQuery<List<GetHallsResponse>>
    {
        public string CacheKey => CacheKeys.Halls(IncludeDeleted);

        public TimeSpan? Expiration => TimeSpan.FromHours(4);

        public IEnumerable<string>? Tags =>
        [
            CacheTags.Halls
        ];
    }
}
