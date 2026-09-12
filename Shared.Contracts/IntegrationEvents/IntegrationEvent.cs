namespace Shared.Contracts.IntegrationEvents
{
    public abstract class IntegrationEvent
        : IIntegrationEvent
    {
        public Guid EventId { get; } = Guid.CreateVersion7();
        public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
    }
}
