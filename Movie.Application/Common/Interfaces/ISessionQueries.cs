using Movie.Application.Features.Sessions.Queries.GetSession;
using Movie.Application.Features.Sessions.Queries.GetSessions;
using Movie.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Common.Interfaces
{
    public interface ISessionQueries
    {
        Task<GetSessionResponse?> GetSessionAsync(Guid Id,
            CancellationToken cancellationToken = default);

        Task<List<GetSessionsResponse>> GetSessionsAsync(
            DateTime? fromUtc,
            DateTime? toUtc,
            Guid? movieId,
            Guid? hallId,
            SessionStatus? status,
            bool includeDeleted,
            CancellationToken cancellationToken = default);
    }
}
