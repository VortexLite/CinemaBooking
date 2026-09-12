using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Application.Common.Constants;
using Movie.Domain.Common;
using Movie.Domain.Entities.Session;
using Movie.Domain.Enums;
using Shared.Common.Exceptions;

namespace Movie.Application.Features.Sessions.Commands.RestoreSession
{
    public sealed class RestoreSessionCommandHandler
        : IRequestHandler<RestoreSessionCommand>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IHallRepository _hallRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TimeProvider _timeProvider;

        public RestoreSessionCommandHandler(ISessionRepository sessionRepository,
            IMovieRepository movieRepository,
            IHallRepository hallRepository,
            IUnitOfWork unitOfWork, 
            TimeProvider timeProvider)
        {
            _sessionRepository = sessionRepository;
            _unitOfWork = unitOfWork;
            _timeProvider = timeProvider;
            _movieRepository = movieRepository;
            _hallRepository = hallRepository;
        }

        public async Task Handle(RestoreSessionCommand request, 
            CancellationToken cancellationToken)
        {
            var session = await _sessionRepository.GetByIdIncludingDeletedAsync(
                request.Id, cancellationToken);

            if (session is null)
                throw new NotFoundException(nameof(Session), request.Id);

            if (!session.IsDeleted)
                return;

            var utcNow = _timeProvider.GetUtcNow().UtcDateTime;

            if (session.StartTime <= utcNow)
                throw new BusinessRuleException("A past session cannot be restored.");

            var hasConflict = await _sessionRepository.HasConflictAsync(
                session.HallId, 
                session.StartTime, 
                session.EndTime,
                SessionRules.CleaningBuffer,
                session.Id,
                cancellationToken);

            if (hasConflict)
                throw new BusinessRuleException("The session cannot be restored because the hall is already occupied during this time period.");

            var movie = await _movieRepository.GetByIdAsync(session.MovieId, cancellationToken);

            if (movie is null)
                throw new BusinessRuleException("Session cannot be restored because its movie is unavailable.");

            if (movie.Status != MovieStatus.Published)
                throw new BusinessRuleException("Session cannot be restored because its movie is not published.");

            var hall = await _hallRepository.GetByIdAsync(session.HallId, cancellationToken);

            if (hall is null)
                throw new BusinessRuleException("Session cannot be restored because its hall is unavailable.");

            session.Restore();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
