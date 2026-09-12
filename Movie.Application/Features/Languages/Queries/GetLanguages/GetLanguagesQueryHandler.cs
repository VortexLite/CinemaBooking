using MediatR;
using Movie.Application.Common.Interfaces;

namespace Movie.Application.Features.Languages.Queries.GetLanguages
{
    public sealed class GetLanguagesQueryHandler
        : IRequestHandler<GetLanguagesQuery, List<GetLanguagesResponse>>
    {
        private readonly ILanguageQueries _languageQueries;

        public GetLanguagesQueryHandler(ILanguageQueries languageQueries)
        {
            _languageQueries = languageQueries;
        }

        public async Task<List<GetLanguagesResponse>> Handle(GetLanguagesQuery request, 
            CancellationToken cancellationToken)
        {
            return await _languageQueries.GetLanguagesAsync(request.IncludeDeleted, cancellationToken);
        }
    }
}
