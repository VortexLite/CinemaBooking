using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Shared.Common.Exceptions;

namespace Movie.Application.Features.Movies.Commands.ArchiveMovie
{
    public sealed class ArchiveMovieCommandHandler
        : IRequestHandler<ArchiveMovieCommand>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TimeProvider _timeProvider;

        public ArchiveMovieCommandHandler(IMovieRepository movieRepository, 
            IUnitOfWork unitOfWork,
            TimeProvider timeProvider)
        {
            _movieRepository = movieRepository;
            _unitOfWork = unitOfWork;
            _timeProvider = timeProvider;
        }

        public async Task Handle(ArchiveMovieCommand request, 
            CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetByIdAsync(request.Id, cancellationToken);

            if (movie is null)
                throw new NotFoundException(nameof(Movie.Domain.Entities.Movie.Movie), request.Id);

            var utcNow = _timeProvider.GetUtcNow().UtcDateTime;

            if (await _movieRepository.HasActiveOrFutureSessionsAsync(movie.Id, utcNow, cancellationToken))
                throw new BusinessRuleException("Movie cannot be archived because it has active or upcoming sessions.");

            movie.Archive();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
