using Movie.Domain.Common;
using Shared.Contracts.IntegrationEvents;

namespace Movie.Application.Common.Interfaces
{
    public interface IDomainEventToIntegrationEventMapper
    {
        IIntegrationEvent? Map(IDomainEvent domainEvent);
    }
}
