using Movie.Domain.Entities.Hall;

namespace Movie.Application.Abstractions.Interfaces
{
    public interface IHallRepository
        : IBaseRepository<Hall>
    {
        Task<Hall?> GetWithSeatsAndSessionsAsync(Guid id,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsByNameAsync(string name,
            CancellationToken cancellationToken= default);

        Task<Hall?> GetByIdIncludingDeletedAsync(Guid id,
            CancellationToken cancellationToken = default);

        Task<bool> HasFutureSessionsAsync(Guid hallId,
            DateTime utcNow,
            CancellationToken cancellationToken = default);

        Task<Hall?> GetWithSeatsAsync(Guid id,
            CancellationToken cancellationToken = default);
    }
}
