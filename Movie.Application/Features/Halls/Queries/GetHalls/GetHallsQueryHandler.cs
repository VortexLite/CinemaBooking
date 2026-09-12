using MediatR;
using Movie.Application.Common.Interfaces;

namespace Movie.Application.Features.Halls.Queries.GetHalls
{
    public sealed class GetHallsQueryHandler
        : IRequestHandler<GetHallsQuery, List<GetHallsResponse>>
    {
        private readonly IHallQueries _hallQueries;

        public GetHallsQueryHandler(IHallQueries hallQueries)
        {
            _hallQueries = hallQueries;
        }

        public async Task<List<GetHallsResponse>> Handle(GetHallsQuery request, 
            CancellationToken cancellationToken)
        {
            return await _hallQueries.GetHallsAsync(request.IncludeDeleted, cancellationToken);
        }
    }
}
