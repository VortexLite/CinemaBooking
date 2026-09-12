using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Application.Common.Constants;
using Movie.Domain.Common;
using Movie.Domain.Entities.Hall;
using Movie.Domain.Entities.Session;
using Movie.Domain.Enums;
using Shared.Common.Exceptions;

namespace Movie.Application.Features.Sessions.Commands.CreateSession
{
    public sealed class CreateSessionCommandHandler
        : IRequestHandler<CreateSessionCommand, Guid>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IHallRepository _hallRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TimeProvider _timeProvider;

        public CreateSessionCommandHandler(ISessionRepository sessionRepository,
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

        public async Task<Guid> Handle(CreateSessionCommand request, 
            CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetByIdAsync(request.MovieId, cancellationToken);

            if (movie is null)
                throw new NotFoundException(nameof(Domain.Entities.Movie.Movie), request.MovieId);

            if (movie.Status != MovieStatus.Published)
                throw new BusinessRuleException("Sessions can only be created for published movies.");

            var hall = await _hallRepository.GetByIdAsync(request.HallId, cancellationToken);

            if (hall is null)
                throw new NotFoundException(nameof(Hall), request.HallId);

            var utcNow = _timeProvider.GetUtcNow().UtcDateTime;

            if (request.StartTime <= utcNow)
                throw new BusinessRuleException("Session must start in the future.");

            var endTime = request.StartTime.AddMinutes(movie.DurationMinutes);

            var hasConflict = await _sessionRepository.HasConflictAsync(
                hall.Id,
                request.StartTime,
                endTime,
                SessionRules.CleaningBuffer,
                cancellationToken: cancellationToken);

            if (hasConflict)
                throw new BusinessRuleException("The hall already has a session during this time period.");

            var session = new Session(
                movie.Id,
                hall.Id,
                request.StartTime,
                endTime,
                request.Price,
                request.ProjectionType,
                request.SubtitleType,
                request.SoundFormat);

            await _sessionRepository.AddAsync(session, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return session.Id;
        }
    }
}
