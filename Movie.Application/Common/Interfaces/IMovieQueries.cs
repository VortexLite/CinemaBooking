using Movie.Application.Features.Movies.Queries.GetDeletedMovies;
using Movie.Application.Features.Movies.Queries.GetMovie;
using Movie.Application.Features.Movies.Queries.GetMovies;
using Shared.Common.Models;

namespace Movie.Application.Common.Interfaces
{
    public interface IMovieQueries
    {
        Task<GetMovieResponse?> GetMovieAsync(Guid id,
        CancellationToken cancellationToken = default);

        Task<PagedResult<GetMoviesResponse>> GetMoviesAsync(GetMoviesQuery query,
            CancellationToken cancellationToken = default);

        Task<PagedResult<GetMoviesResponse>> GetDeletedMoviesAsync(GetDeletedMoviesQuery request,
            CancellationToken cancellationToken = default);
    }
}
