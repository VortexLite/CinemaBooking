using MediatR;
using Movie.Application.Common.Interfaces;
using Movie.Application.Features.Movies.Queries.GetMovies;
using Shared.Common.Models;

namespace Movie.Application.Features.Movies.Queries.GetDeletedMovies
{
    public sealed class GetDeletedMoviesQueryHandler
        : IRequestHandler<GetDeletedMoviesQuery, PagedResult<GetMoviesResponse>>
    {
        private readonly IMovieQueries _movieQuery;

        public GetDeletedMoviesQueryHandler(IMovieQueries movieQuery)
        {
            _movieQuery = movieQuery;
        }

        public async Task<PagedResult<GetMoviesResponse>> Handle(GetDeletedMoviesQuery request, 
            CancellationToken cancellationToken)
        {
            return await _movieQuery.GetDeletedMoviesAsync(request, cancellationToken);
        }
    }
}
