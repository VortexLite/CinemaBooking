using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Application.Caching;
using Movie.Domain.Events.Movies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Movies.Events
{
    public sealed class MovieCacheInvalidationHandler :
        INotificationHandler<MovieCreatedEvent>,
        INotificationHandler<MovieUpdatedEvent>,
        INotificationHandler<MovieDeletedEvent>,
        INotificationHandler<MoviePublishedEvent>,
        INotificationHandler<MovieRestoredEvent>    
    {
        private readonly ICacheService _cacheService;

        public MovieCacheInvalidationHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        private async Task InvalidateMovieAsync(
        Guid movieId,
        CancellationToken cancellationToken)
        {
            await _cacheService.RemoveByTagAsync(
                CacheTags.Movie(movieId),
                cancellationToken);

            await _cacheService.RemoveByTagAsync(
                CacheTags.Movies,
                cancellationToken);
        }

        private Task InvalidateMoviesAsync(
            CancellationToken cancellationToken)
        {
            return _cacheService.RemoveByTagAsync(
                CacheTags.Movies,
                cancellationToken);
        }

        public Task Handle(
        MovieCreatedEvent notification,
        CancellationToken cancellationToken)
        {
            return InvalidateMoviesAsync(cancellationToken);
        }

        public Task Handle(
            MovieUpdatedEvent notification,
            CancellationToken cancellationToken)
        {
            return InvalidateMovieAsync(
                notification.MovieId,
                cancellationToken);
        }

        public Task Handle(
            MovieDeletedEvent notification,
            CancellationToken cancellationToken)
        {
            return InvalidateMovieAsync(
                notification.MovieId,
                cancellationToken);
        }

        public Task Handle(
            MoviePublishedEvent notification,
            CancellationToken cancellationToken)
        {
            return InvalidateMovieAsync(
                notification.MovieId,
                cancellationToken);
        }

        public Task Handle(
            MovieRestoredEvent notification,
            CancellationToken cancellationToken)
        {
            return InvalidateMovieAsync(
                notification.MovieId,
                cancellationToken);
        }
    }
}
