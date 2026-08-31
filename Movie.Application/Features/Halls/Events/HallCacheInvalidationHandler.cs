using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Application.Caching;
using Movie.Domain.Events.Hall;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Halls.Events
{
    public sealed class HallCacheInvalidationHandler :
        INotificationHandler<HallCreatedEvent>,
        INotificationHandler<HallUpdatedEvent>,
        INotificationHandler<HallDeletedEvent>,
        INotificationHandler<HallRestoredEvent>
    {
        private readonly ICacheService _cache;

        public HallCacheInvalidationHandler(ICacheService cache)
        {
            _cache = cache;
        }

        public Task Handle(HallCreatedEvent notification, 
            CancellationToken cancellationToken)
        {
            return _cache.RemoveByTagAsync(
                CacheTags.Halls,
                cancellationToken);
        }

        public Task Handle(HallUpdatedEvent notification,
            CancellationToken cancellationToken)
        {
            return InvalidateAsync(
            notification.HallId,
            cancellationToken);
        }

        public Task Handle(HallDeletedEvent notification, 
            CancellationToken cancellationToken)
        {
            return InvalidateAsync(
            notification.HallId,
            cancellationToken);
        }

        public Task Handle(HallRestoredEvent notification, 
            CancellationToken cancellationToken)
        {
            return InvalidateAsync(
            notification.HallId,
            cancellationToken);
        }

        private async Task InvalidateAsync(
        Guid hallId,
        CancellationToken cancellationToken)
        {
            await _cache.RemoveByTagAsync(
                CacheTags.Hall(hallId),
                cancellationToken);

            await _cache.RemoveByTagAsync(
                CacheTags.Halls,
                cancellationToken);

            await _cache.RemoveByTagAsync(
                CacheTags.Sessions,
                cancellationToken);
        }
    }
}
