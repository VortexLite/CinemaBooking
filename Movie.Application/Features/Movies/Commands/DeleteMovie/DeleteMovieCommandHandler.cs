using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Shared.Common.Exceptions;

namespace Movie.Application.Features.Movies.Commands.DeleteMovie
{
    public sealed class DeleteMovieCommandHandler
        : IRequestHandler<DeleteMovieCommand>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly TimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMovieCommandHandler(IMovieRepository movieRepository, 
            TimeProvider timeProvider,
            IUnitOfWork unitOfWork)
        {
            _movieRepository = movieRepository;
            _timeProvider = timeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteMovieCommand request, 
            CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetByIdAsync(request.Id, cancellationToken);

            if (movie is null)
                throw new NotFoundException(nameof(Domain.Entities.Movie.Movie), request.Id);

            var utcNow = _timeProvider.GetUtcNow().UtcDateTime;

            if (await _movieRepository.HasActiveOrFutureSessionsAsync(movie.Id, utcNow, cancellationToken))
                throw new BusinessRuleException("Movie cannot be deleted while future sessions exist.");

            movie.Delete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
