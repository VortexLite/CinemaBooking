using MediatR;
using Movie.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Sessions.Queries.GetSessions
{
    public sealed class GetSessionsQueryHandler
        : IRequestHandler<GetSessionsQuery, List<GetSessionsResponse>>
    {
        private readonly ISessionQueries _sessionQueries;

        public GetSessionsQueryHandler(ISessionQueries sessionQueries)
        {
            _sessionQueries = sessionQueries;
        }

        public async Task<List<GetSessionsResponse>> Handle(GetSessionsQuery request, 
            CancellationToken cancellationToken)
        {
            return await _sessionQueries.GetSessionsAsync(
                request.FromUtc,
                request.ToUtc,
                request.MovieId,
                request.HallId,
                request.Status,
                request.IncludeDeleted,
                cancellationToken);
        }
    }
}
