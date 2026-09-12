using Movie.Domain.Entities.Seat;

namespace Movie.Application.Abstractions.Interfaces
{
    public interface ISeatRepository
        : IBaseRepository<Seat>
    {
        Task<List<Seat>> GetByHallAsync(Guid hallId,
            CancellationToken cancellationToken = default);

        Task<Seat?> GetByPositionAsync(Guid hallId,
            ushort row,
            ushort number,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(Guid hallId,
            ushort row,
            ushort number,
            CancellationToken cancellationToken = default);

        Task<int> CountAsync(Guid hallId,
            CancellationToken cancellationToken = default);
    }
}
