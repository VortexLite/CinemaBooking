using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Application.Caching;
using Movie.Domain.Events.Genre;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Genres.Events
{
    public sealed class GenreCacheInvalidationHandler: 
        INotificationHandler<GenreCreatedEvent>,
        INotificationHandler<GenreUpdatedEvent>,
        INotificationHandler<GenreDeletedEvent>,
        INotificationHandler<GenreRestoredEvent>
    {
        private readonly ICacheService _cacheService;

        public GenreCacheInvalidationHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public Task Handle(GenreCreatedEvent notification, 
            CancellationToken cancellationToken)
        {
            return InvalidateGenresAsync(cancellationToken);
        }

        public Task Handle(GenreUpdatedEvent notification, 
            CancellationToken cancellationToken)
        {
            return InvalidateGenreAsync(notification.GenreId, cancellationToken);
        }

        public Task Handle(GenreDeletedEvent notification, 
            CancellationToken cancellationToken)
        {
            return InvalidateGenreAsync(notification.GenreId, cancellationToken);
        }

        public Task Handle(GenreRestoredEvent notification,
            CancellationToken cancellationToken)
        {
            return InvalidateGenreAsync(notification.GenreId, cancellationToken);
        }

        private async Task InvalidateGenreAsync(Guid genreId,
            CancellationToken cancellationToken)
        {
            await _cacheService.RemoveByTagAsync(
                CacheTags.Genre(genreId),
                cancellationToken);

            await _cacheService.RemoveByTagAsync(
                CacheTags.Genres,
                cancellationToken);
        }

        private Task InvalidateGenresAsync(
        CancellationToken cancellationToken)
        {
            return _cacheService.RemoveByTagAsync(
                CacheTags.Genres,
                cancellationToken);
        }
    }
}
