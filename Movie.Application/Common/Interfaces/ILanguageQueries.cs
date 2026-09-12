using Movie.Application.Features.Languages.Queries.GetLanguage;
using Movie.Application.Features.Languages.Queries.GetLanguages;

namespace Movie.Application.Common.Interfaces
{
    public interface ILanguageQueries
    {
        Task<GetLanguageResponse?> GetLanguageAsync(Guid id,
            CancellationToken cancellationToken = default);

        Task<List<GetLanguagesResponse>> GetLanguagesAsync(bool includeDeleted,
            CancellationToken cancellationToken = default);
    }
}
