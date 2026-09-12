using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Application.Caching;
using Movie.Domain.Events.Movies;

namespace Movie.Application.Features.Movies.Events
{
    public sealed class MovieCacheInvalidationHandler :
        INotificationHandler<MovieCreatedEvent>,
        INotificationHandler<MoviePublishedEvent>,
        INotificationHandler<MovieArchivedEvent>,
        INotificationHandler<MovieDeletedEvent>,
        INotificationHandler<MovieRestoredEvent>,

        INotificationHandler<MovieTitleChangedEvent>,
        INotificationHandler<MovieDescriptionChangedEvent>,
        INotificationHandler<MovieDurationChangedEvent>,
        INotificationHandler<MovieAgeRestrictionChangedEvent>,
        INotificationHandler<MoviePosterChangedEvent>,
        INotificationHandler<MovieTrailerChangedEvent>,
        INotificationHandler<MovieReleaseDateChangedEvent>,
        INotificationHandler<MovieLanguageChangedEvent>,
        INotificationHandler<MovieGenreAddedEvent>,
        INotificationHandler<MovieGenreRemovedEvent>
    {
        private readonly ICacheService _cacheService;

        public MovieCacheInvalidationHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task Handle(MovieCreatedEvent notification,
            CancellationToken cancellationToken)
        {
            await _cacheService.RemoveByTagAsync(CacheTags.Movies, cancellationToken);
        }

        public async Task Handle(MovieDeletedEvent notification,
            CancellationToken cancellationToken)
        {
            await InvalidateMovieAndSessionsAsync(notification.MovieId, cancellationToken);
        }

        public async Task Handle(MoviePublishedEvent notification,
            CancellationToken cancellationToken)
        {
            await InvalidateMovieAndSessionsAsync(notification.MovieId, cancellationToken);
        }

        public async Task Handle(MovieRestoredEvent notification,
            CancellationToken cancellationToken)
        {
            await InvalidateMovieAndSessionsAsync(notification.MovieId, cancellationToken);
        }

        public async Task Handle(MovieAgeRestrictionChangedEvent notification, 
            CancellationToken cancellationToken)
        {
            await InvalidateMovieAsync(notification.MovieId, cancellationToken);
        }

        public async Task Handle(MovieArchivedEvent notification, 
            CancellationToken cancellationToken)
        {
            await InvalidateMovieAndSessionsAsync(notification.MovieId, cancellationToken);
        }

        public async Task Handle(MovieDescriptionChangedEvent notification, 
            CancellationToken cancellationToken)
        {
            await InvalidateMovieAsync(notification.MovieId, cancellationToken);
        }

        public async Task Handle(MovieDurationChangedEvent notification,
            CancellationToken cancellationToken)
        {
            await InvalidateMovieAsync(notification.MovieId, cancellationToken);
        }

        public async Task Handle(MovieGenreAddedEvent notification, 
            CancellationToken cancellationToken)
        {
            await InvalidateMovieAsync(notification.MovieId, cancellationToken);
        }

        public async Task Handle(MovieGenreRemovedEvent notification,
            CancellationToken cancellationToken)
        {
            await InvalidateMovieAsync(notification.MovieId, cancellationToken);
        }

        public async Task Handle(MovieLanguageChangedEvent notification,
            CancellationToken cancellationToken)
        {
            await InvalidateMovieAsync(notification.MovieId, cancellationToken);
        }

        public async Task Handle(MoviePosterChangedEvent notification, 
            CancellationToken cancellationToken)
        {
            await InvalidateMovieAsync(notification.MovieId, cancellationToken);
        }

        public async Task Handle(MovieTitleChangedEvent notification, 
            CancellationToken cancellationToken)
        {
            await InvalidateMovieAndSessionsAsync(notification.MovieId, cancellationToken);
        }

        public async Task Handle(MovieTrailerChangedEvent notification,
            CancellationToken cancellationToken)
        {
            await InvalidateMovieAsync(notification.MovieId, cancellationToken);
        }

        public async Task Handle(MovieReleaseDateChangedEvent notification, 
            CancellationToken cancellationToken)
        {
            await InvalidateMovieAsync(notification.MovieId, cancellationToken);
        }

        private async Task InvalidateMovieAsync(Guid movieId,
            CancellationToken cancellationToken)
        {
            await _cacheService.RemoveByTagAsync(CacheTags.Movie(movieId), cancellationToken);

            await _cacheService.RemoveByTagAsync(CacheTags.Movies, cancellationToken);
        }

        private async Task InvalidateMovieAndSessionsAsync(Guid movieId,
            CancellationToken cancellationToken)
        {
            await InvalidateMovieAsync(movieId, cancellationToken);

            await _cacheService.RemoveByTagAsync(CacheTags.Sessions, cancellationToken);
        }
    }
}
