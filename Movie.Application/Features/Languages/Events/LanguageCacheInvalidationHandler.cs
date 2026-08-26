using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Application.Caching;
using Movie.Domain.Events.Language;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Languages.Events
{
    public sealed class LanguageCacheInvalidationHandler :
        INotificationHandler<LanguageCreatedEvent>,
        INotificationHandler<LanguageUpdatedEvent>,
        INotificationHandler<LanguageDeletedEvent>,
        INotificationHandler<LanguageRestoredEvent>
    {
        private readonly ICacheService _cache;

        public LanguageCacheInvalidationHandler(ICacheService cache)
        {
            _cache = cache;
        }

        public Task Handle(LanguageCreatedEvent notification, 
            CancellationToken cancellationToken)
        {
            return _cache.RemoveByTagAsync(
                CacheTags.Languages,
                cancellationToken);
        }

        public Task Handle(LanguageUpdatedEvent notification, 
            CancellationToken cancellationToken)
        {
            return InvalidateAsync(notification.LanguageId, cancellationToken);
        }

        public Task Handle(LanguageDeletedEvent notification, 
            CancellationToken cancellationToken)
        {
            return InvalidateAsync(notification.LanguageId, cancellationToken);
        }

        public Task Handle(LanguageRestoredEvent notification, 
            CancellationToken cancellationToken)
        {
            return InvalidateAsync(notification.LanguageId, cancellationToken);
        }

        private async Task InvalidateAsync(
        Guid languageId,
        CancellationToken cancellationToken)
        {
            await _cache.RemoveByTagAsync(
                CacheTags.Language(languageId),
                cancellationToken);

            await _cache.RemoveByTagAsync(
                CacheTags.Languages,
                cancellationToken);

            await _cache.RemoveByTagAsync(
                CacheTags.Movies,
                cancellationToken);
        }
    }
}
