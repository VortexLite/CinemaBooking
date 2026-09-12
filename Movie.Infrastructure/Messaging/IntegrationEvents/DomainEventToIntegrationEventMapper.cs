using Movie.Application.Common.Interfaces;
using Movie.Domain.Common;
using Movie.Domain.Events.Movies;
using Movie.Domain.Events.Session;
using Shared.Contracts.IntegrationEvents;
using Shared.Contracts.IntegrationEvents.Movies;
using Shared.Contracts.IntegrationEvents.Sessions;

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

                SessionBookingOpenedEvent e =>
                new SessionBookingOpenedIntegrationEvent(
                    e.SessionId,
                    e.MovieId,
                    e.HallId,
                    e.StartTime,
                    e.EndTime,
                    e.Price),

                SessionCancelledEvent e =>
                    new SessionCancelledIntegrationEvent(
                        e.SessionId,
                        e.Reason),

                SessionRescheduledEvent e =>
                    new SessionRescheduledIntegrationEvent(
                        e.SessionId,
                        e.HallId,
                        e.StartTime,
                        e.EndTime),

                SessionHallChangedEvent e =>
                    new SessionHallChangedIntegrationEvent(
                        e.SessionId,
                        e.HallId),

                _ => null
            };
        }
    }
}
