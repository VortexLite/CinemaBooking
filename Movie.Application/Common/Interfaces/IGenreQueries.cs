using Movie.Application.Features.Genres.Queries.GetGenre;
using Movie.Application.Features.Genres.Queries.GetGenres;

namespace Movie.Application.Common.Interfaces
{
    public interface IGenreQueries
    {
        Task<GetGenreResponse?> GetGenreAsync(Guid id,
            CancellationToken cancellationToken = default);

        Task<List<GetGenresResponse>> GetGenresAsync(bool includeDeleted,
            CancellationToken cancellationToken = default);
    }
}
