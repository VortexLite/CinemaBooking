namespace Movie.Infrastructure.Messaging.Outbox
{
    public interface IOutboxCleanupService
    {
        Task CleanupAsync(CancellationToken cancellationToken = default);
    }
}
