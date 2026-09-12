using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Application.Common.Constants;
using Movie.Domain.Common;
using Movie.Domain.Entities.Hall;
using Movie.Domain.Entities.Session;
using Shared.Common.Exceptions;

namespace Movie.Application.Features.Sessions.Commands.UpdateSession
{
    public sealed class UpdateSessionCommandHandler
        : IRequestHandler<UpdateSessionCommand>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IHallRepository _hallRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TimeProvider _timeProvider;

        public UpdateSessionCommandHandler(ISessionRepository sessionRepository,
            IMovieRepository movieRepository, 
            IHallRepository hallRepository, 
            IUnitOfWork unitOfWork, 
            TimeProvider timeProvider)
        {
            _sessionRepository = sessionRepository;
            _movieRepository = movieRepository;
            _hallRepository = hallRepository;
            _unitOfWork = unitOfWork;
            _timeProvider = timeProvider;
        }

        public async Task Handle(UpdateSessionCommand request, 
            CancellationToken cancellationToken)
        {
            var session = await _sessionRepository.GetByIdAsync(request.Id, cancellationToken);

            if (session is null)
                throw new NotFoundException(nameof(Session), request.Id);

            var utcNow = _timeProvider.GetUtcNow().UtcDateTime;

            var startTimeChanged = session.StartTime != request.StartTime;

            var hallChanged = session.HallId != request.HallId;

            if (startTimeChanged && request.StartTime <= utcNow)
                throw new BusinessRuleException("Session must start in the future.");

            if (hallChanged)
            {
                var hall = await _hallRepository.GetByIdAsync(request.HallId,
                    cancellationToken);

                if (hall is null)
                    throw new NotFoundException(nameof(Hall), request.HallId);
            }

            var endTime = session.EndTime;

            if (startTimeChanged)
            {
                var movie = await _movieRepository.GetByIdAsync(session.MovieId,
                    cancellationToken);

                if (movie is null)
                    throw new NotFoundException(nameof(Domain.Entities.Movie.Movie), session.MovieId);

                endTime = request.StartTime.AddMinutes(movie.DurationMinutes);
            }

            if (startTimeChanged || hallChanged)
            {
                var hasConflict = await _sessionRepository.HasConflictAsync(
                        request.HallId,
                        request.StartTime,
                        endTime,
                        SessionRules.CleaningBuffer,
                        session.Id,
                        cancellationToken);

                if (hasConflict)
                    throw new BusinessRuleException("The hall already has a session during this time period.");
            }

            session.Update(
                request.HallId,
                request.StartTime,
                endTime,
                request.Price,
                request.ProjectionType,
                request.SubtitleType,
                request.SoundFormat);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);
        }
    }
}
