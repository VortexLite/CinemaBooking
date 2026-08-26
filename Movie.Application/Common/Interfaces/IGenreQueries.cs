using Movie.Application.Features.Genres.Queries.GetGenre;
using Movie.Application.Features.Genres.Queries.GetGenres;
using System;
using System.Collections.Generic;
using System.Text;

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
