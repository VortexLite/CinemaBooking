using Movie.Application.Caching;
using Movie.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Sessions.Queries.GetSession
{
    public sealed record GetSessionQuery(Guid Id)
        : ICachedQuery<GetSessionResponse?>
    {
        public string CacheKey => CacheKeys.Session(Id);

        public TimeSpan? Expiration => TimeSpan.FromHours(2);

        public IEnumerable<string>? Tags =>
        [
            CacheTags.Sessions,
            CacheTags.Session(Id)
        ];
    }
}
