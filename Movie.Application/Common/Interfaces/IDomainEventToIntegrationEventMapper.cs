using Movie.Domain.Common;
using Shared.Contracts.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Common.Interfaces
{
    public interface IDomainEventToIntegrationEventMapper
    {
        IIntegrationEvent? Map(IDomainEvent domainEvent);
    }
}
