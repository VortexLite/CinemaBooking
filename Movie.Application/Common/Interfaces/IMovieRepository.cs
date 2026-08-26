using Movie.Application.Features.Movies.Queries.GetMovie;
using Movie.Domain.Entities.Movie;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Abstractions.Interfaces
{
    public interface IMovieRepository
        : IBaseRepository<Domain.Entities.Movie.Movie>
    {
        Task<Domain.Entities.Movie.Movie?> GetDetailsAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<Domain.Entities.Movie.Movie?> GetByTitleAsync(
            string title,
            CancellationToken cancellationToken = default);

        Task<bool> ExistAsync(
            string title,
            CancellationToken cancellationToken = default);

        Task<List<Domain.Entities.Movie.Movie>> GetPublishAsync(
            CancellationToken cancellationToken = default);

        Task<Domain.Entities.Movie.Movie?> GetDeletedByIdAsync(Guid id,
            CancellationToken cancellationToken = default);

        Task RestoreAsync(Guid id,
            CancellationToken cancellationToken = default);

        Task<List<Domain.Entities.Movie.Movie>> GetDeletedAsync(
            CancellationToken cancellationToken = default);
    }
}
