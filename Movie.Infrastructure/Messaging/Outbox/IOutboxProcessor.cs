namespace Movie.Infrastructure.Messaging.Outbox
{
    public interface IOutboxProcessor
    {
        Task ProcessAsync(CancellationToken cancellationToken = default);
    }
}
