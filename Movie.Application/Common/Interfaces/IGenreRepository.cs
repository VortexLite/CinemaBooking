using Movie.Domain.Entities.Genre;

namespace Movie.Application.Abstractions.Interfaces
{
    public interface IGenreRepository
        : IBaseRepository<Genre>
    {
        Task<List<Genre>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<Genre?> GetByNameAsync(
            string name,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsByNameAsync(string name,
            CancellationToken cancellationToken = default);

        Task<bool> ExistAsync(Guid genreId,
            CancellationToken cancellationToken = default);
        
        Task<Genre?> GetByIdIncludingDeletedAsync(Guid id,
            CancellationToken cancellationToken = default);
        Task<bool> AllExistAsync(IReadOnlyCollection<Guid> ids,
            CancellationToken cancellationToken = default);

        Task<bool> IsUsedByProtectedMoviesAsync(Guid genreId,
            CancellationToken cancellationToken = default);
    }
}
