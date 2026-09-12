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

        public async Task Handle(GenreCreatedEvent notification, 
            CancellationToken cancellationToken)
        {
            await InvalidateGenresAsync(cancellationToken);
        }

        public async Task Handle(GenreUpdatedEvent notification, 
            CancellationToken cancellationToken)
        {
            await InvalidateGenreAsync(notification.GenreId, cancellationToken);
        }

        public async Task Handle(GenreDeletedEvent notification, 
            CancellationToken cancellationToken)
        {
            await InvalidateGenreAsync(notification.GenreId, cancellationToken);
        }

        public async Task Handle(GenreRestoredEvent notification,
            CancellationToken cancellationToken)
        {
            await InvalidateGenreAsync(notification.GenreId, cancellationToken);
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
