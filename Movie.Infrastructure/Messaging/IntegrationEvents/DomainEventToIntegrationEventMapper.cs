using Movie.Application.Common.Interfaces;
using Movie.Domain.Common;
using Movie.Domain.Events.Movies;
using Shared.Contracts.IntegrationEvents;
using Shared.Contracts.IntegrationEvents.Movies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Messaging.IntegrationEvents
{
    public sealed class DomainEventToIntegrationEventMapper
        : IDomainEventToIntegrationEventMapper
    {
        public IIntegrationEvent? Map(IDomainEvent domainEvent)
        {
            return domainEvent switch
            {
                MoviePublishedEvent @event => new MoviePublishedIntegrationEvent(
                    @event.MovieId,
                    @event.Title),

                _ => null
            };
        }
    }
}
