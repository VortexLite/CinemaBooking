using MediatR;
using Movie.Application.Common.Interfaces;
using Movie.Domain.Entities.Hall;
using Shared.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Halls.Queries.GetHall
{
    public sealed class GetHallQueryHandler
        : IRequestHandler<GetHallQuery, GetHallResponse>
    {
        private readonly IHallQueries _hallQueries;

        public GetHallQueryHandler(IHallQueries hallQueries)
        {
            _hallQueries = hallQueries;
        }

        public async Task<GetHallResponse> Handle(GetHallQuery request,
            CancellationToken cancellationToken)
        {
            return await _hallQueries.GetHallAsync(
                request.Id,
                cancellationToken)
                ?? throw new NotFoundException(nameof(Hall), request.Id);
        }
    }
}
