using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Application.Caching;
using Movie.Domain.Events.Session;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Sessions.Events
{
    public sealed class SessionCacheInvalidationHandler :
        INotificationHandler<SessionCreatedEvent>,
        INotificationHandler<SessionDeletedEvent>,
        INotificationHandler<SessionRestoredEvent>,
        INotificationHandler<SessionBookingOpenedEvent>,
        INotificationHandler<SessionCancelledEvent>,
        INotificationHandler<SessionStartedEvent>,
        INotificationHandler<SessionFinishedEvent>,
        INotificationHandler<SessionRescheduledEvent>,
        INotificationHandler<SessionPriceChangedEvent>,
        INotificationHandler<SessionHallChangedEvent>
    {
        private readonly ICacheService _cache;

        public SessionCacheInvalidationHandler(ICacheService cache)
        {
            _cache = cache;
        }

        public async Task Handle(SessionCreatedEvent notification, 
            CancellationToken cancellationToken)
        {
            await _cache.RemoveByTagAsync(CacheTags.Sessions,cancellationToken);
        }

        public async Task Handle(SessionDeletedEvent notification, 
            CancellationToken cancellationToken)
        {
            await InvalidateSessionAsync(notification.SessionId, cancellationToken);
        }

        public async Task Handle(SessionRestoredEvent notification, 
            CancellationToken cancellationToken)
        {
            await InvalidateSessionAsync(notification.SessionId, cancellationToken);
        }

        public async Task Handle(SessionBookingOpenedEvent notification, 
            CancellationToken cancellationToken)
        {
            await InvalidateSessionAsync(notification.SessionId, cancellationToken);
        }

        public async Task Handle(SessionCancelledEvent notification, 
            CancellationToken cancellationToken)
        {
            await InvalidateSessionAsync(notification.SessionId, cancellationToken);
        }

        public async Task Handle(SessionStartedEvent notification,
            CancellationToken cancellationToken)
        {
            await InvalidateSessionAsync(notification.SessionId, cancellationToken);
        }

        public async Task Handle(SessionFinishedEvent notification,
            CancellationToken cancellationToken)
        {
            await InvalidateSessionAsync(notification.SessionId, cancellationToken);
        }

        public async Task Handle(SessionRescheduledEvent notification, 
            CancellationToken cancellationToken)
        {
            await InvalidateSessionAsync(notification.SessionId, cancellationToken);
        }

        public async Task Handle(SessionPriceChangedEvent notification, 
            CancellationToken cancellationToken)
        {
            await InvalidateSessionAsync(notification.SessionId, cancellationToken);
        }

        public async Task Handle(SessionHallChangedEvent notification, 
            CancellationToken cancellationToken)
        {
            await InvalidateSessionAsync(notification.SessionId, cancellationToken);
        }

        private async Task InvalidateSessionAsync(Guid sessionId,
            CancellationToken cancellationToken)
        {
            await _cache.RemoveByTagAsync(
                CacheTags.Session(sessionId),
                cancellationToken);

            await _cache.RemoveByTagAsync(
                CacheTags.Sessions,
                cancellationToken);
        }
    }
}
