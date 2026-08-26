using MediatR;
using Movie.Application.Common.Interfaces;
using Shared.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Languages.Queries.GetLanguage
{
    public sealed class GetLanguageQueryHandler
    : IRequestHandler<GetLanguageQuery, GetLanguageResponse>
    {
        private readonly ILanguageQueries _languageQueries;

        public GetLanguageQueryHandler(
            ILanguageQueries languageQueries)
        {
            _languageQueries = languageQueries;
        }

        public async Task<GetLanguageResponse> Handle(
            GetLanguageQuery request,
            CancellationToken cancellationToken)
        {
            return await _languageQueries.GetLanguageAsync(
                       request.Id,
                       cancellationToken)
                   ?? throw new NotFoundException(
                       nameof(Domain.Entities.Language.Language),
                       request.Id);
        }
    }
}
