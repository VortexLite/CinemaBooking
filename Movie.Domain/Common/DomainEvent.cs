namespace Movie.Domain.Common
{
    public abstract class DomainEvent : IDomainEvent
    {
        public Guid EventId { get; } = Guid.CreateVersion7();
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
