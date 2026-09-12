using MediatR;

namespace Movie.Domain.Common
{
    public interface IDomainEvent : INotification
    {
        Guid EventId { get; }
        DateTime OccurredOn { get; }
    }
}
