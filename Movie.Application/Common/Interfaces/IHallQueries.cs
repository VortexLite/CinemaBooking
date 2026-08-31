using Movie.Application.Features.Halls.Queries.GetHall;
using Movie.Application.Features.Halls.Queries.GetHalls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Common.Interfaces
{
    public interface IHallQueries
    {
        Task<GetHallResponse?> GetHallAsync(Guid id,
            CancellationToken cancellationToken = default);

        Task<List<GetHallsResponse>> GetHallsAsync(bool includeDeleted,
            CancellationToken cancellationToken = default);
    }
}
