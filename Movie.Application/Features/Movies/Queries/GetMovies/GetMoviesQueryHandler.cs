using MediatR;
using Movie.Application.Common.Interfaces;
using Shared.Common.Models;

namespace Movie.Application.Features.Movies.Queries.GetMovies
{
    public sealed class GetMoviesQueryHandler
    : IRequestHandler<GetMoviesQuery, PagedResult<GetMoviesResponse>>
    {
        private readonly IMovieQueries _movieQueries;

        public GetMoviesQueryHandler(
            IMovieQueries movieQueries)
        {
            _movieQueries = movieQueries;
        }

        public async Task<PagedResult<GetMoviesResponse>> Handle(GetMoviesQuery request,
            CancellationToken cancellationToken)
        {
            return await _movieQueries.GetMoviesAsync(request, cancellationToken);
        }
    }
}
