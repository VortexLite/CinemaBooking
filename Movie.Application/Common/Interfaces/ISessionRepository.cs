using Movie.Domain.Entities.Session;

namespace Movie.Application.Abstractions.Interfaces
{
    public interface ISessionRepository
        : IBaseRepository<Session>
    {
        Task<Session?> GetByIdIncludingDeletedAsync(Guid id,
            CancellationToken cancellationToken = default);

        Task<bool> HasConflictAsync(
            Guid hallId, 
            DateTime startTime,
            DateTime endTime,
            TimeSpan buffer,
            Guid? excludeSessionId = null,
            CancellationToken cancellationToken = default);

        Task<List<Session>> GetSessionToStartAsync(DateTime utcNow, int batchSize,
            CancellationToken cancellationToken = default);

        Task<List<Session>> GetSessionToFinishAsync(DateTime utcNow, int batchSize,
            CancellationToken cancellationToken = default);
    }
}
