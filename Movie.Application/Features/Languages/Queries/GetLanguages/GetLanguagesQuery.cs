using Movie.Application.Caching;
using Movie.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Languages.Queries.GetLanguages
{
    public sealed record GetLanguagesQuery(bool IncludeDeleted = false)
        : ICachedQuery<List<GetLanguagesResponse>>
    {
        public string CacheKey => CacheKeys.Languages(IncludeDeleted);

        public TimeSpan? Expiration => TimeSpan.FromHours(12);

        public IEnumerable<string>? Tags =>
        [
            CacheTags.Languages
        ];
    }
}
