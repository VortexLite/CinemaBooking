namespace Movie.Application.Abstractions.Interfaces
{
    public interface IMovieRepository
        : IBaseRepository<Domain.Entities.Movie.Movie>
    {
        Task<Domain.Entities.Movie.Movie?> GetDetailsAsync(Guid id,
            CancellationToken cancellationToken = default);

        Task<Domain.Entities.Movie.Movie?> GetByTitleAsync(string title,
            CancellationToken cancellationToken = default);

        Task<bool> ExistAsync(string title,
            CancellationToken cancellationToken = default);

        Task<List<Domain.Entities.Movie.Movie>> GetPublishAsync(
            CancellationToken cancellationToken = default);

        Task<Domain.Entities.Movie.Movie?> GetDeletedByIdAsync(Guid id,
            CancellationToken cancellationToken = default);

        Task<List<Domain.Entities.Movie.Movie>> GetDeletedAsync(
            CancellationToken cancellationToken = default);

        Task<bool> HasActiveOrFutureSessionsAsync(Guid movieId,
            DateTime utcNow,
            CancellationToken cancellationToken = default);

        Task<Domain.Entities.Movie.Movie?> GetWithGenresAsync(Guid id,
            CancellationToken cancellationToken = default);
    }
}
