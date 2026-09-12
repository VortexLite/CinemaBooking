using Movie.Application.Caching;
using Movie.Application.Common.Interfaces;

namespace Movie.Application.Features.Languages.Queries.GetLanguage
{
    public sealed record GetLanguageQuery(Guid Id)
        : ICachedQuery<GetLanguageResponse>
    {
        public string CacheKey => CacheKeys.Language(Id);

        public TimeSpan? Expiration => TimeSpan.FromHours(6);

        public IEnumerable<string>? Tags =>
        [
            CacheTags.Languages,
            CacheTags.Language(Id)
        ];
    }
}
