using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Movie.Domain.Entities.Session;
using Shared.Common.Exceptions;

namespace Movie.Application.Features.Sessions.Commands.FinishSession
{
    public sealed class FinishSessionCommandHandler
        : IRequestHandler<FinishSessionCommand>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TimeProvider _timeProvider;

        public FinishSessionCommandHandler(ISessionRepository sessionRepository,
            IUnitOfWork unitOfWork,
            TimeProvider timeProvider)
        {
            _sessionRepository = sessionRepository;
            _unitOfWork = unitOfWork;
            _timeProvider = timeProvider;
        }

        public async Task Handle(FinishSessionCommand request,
            CancellationToken cancellationToken)
        {
            var session = await _sessionRepository.GetByIdAsync(request.Id, cancellationToken);

            if (session is null)
                throw new NotFoundException(nameof(Session), request.Id);

            var utcNow = _timeProvider.GetUtcNow().UtcDateTime;

            if (session.EndTime > utcNow)
                throw new BusinessRuleException("Session cannot be finished before its scheduled end time.");

            session.Finish();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
