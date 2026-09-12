using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Application.Common.Interfaces;
using Movie.Application.Features.Sessions.Queries.GetSession;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Sessions.Queries.GetSession
{
    public sealed class GetSessionQueryHandler
        : IRequestHandler<GetSessionQuery, GetSessionResponse?>
    {
        private readonly ISessionQueries _sessionQueries;

        public GetSessionQueryHandler(ISessionQueries sessionQueries)
        {
            _sessionQueries = sessionQueries;
        }

        public async Task<GetSessionResponse?> Handle(GetSessionQuery request,
            CancellationToken cancellationToken)
        {
            return await _sessionQueries.GetSessionAsync(request.Id, cancellationToken);
        }
    }
}
