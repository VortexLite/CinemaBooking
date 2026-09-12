using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Movie.Domain.Entities.Session;
using Shared.Common.Exceptions;

namespace Movie.Application.Features.Sessions.Commands.OpenSessionBooking
{
    public sealed class OpenSessionBookingCommandHandler
        : IRequestHandler<OpenSessionBookingCommand>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TimeProvider _timeProvider;

        public OpenSessionBookingCommandHandler(ISessionRepository sessionRepository,
            IUnitOfWork unitOfWork,
            TimeProvider timeProvider)
        {
            _sessionRepository = sessionRepository;
            _unitOfWork = unitOfWork;
            _timeProvider = timeProvider;
        }

        public async Task Handle(OpenSessionBookingCommand request,
            CancellationToken cancellationToken)
        {
            var session = await _sessionRepository.GetByIdAsync(request.Id, cancellationToken);

            if (session is null)
                throw new NotFoundException(nameof(Session), request.Id);

            var utcNow = _timeProvider.GetUtcNow().UtcDateTime;

            if (session.StartTime <= utcNow)
                throw new BusinessRuleException("Booking cannot be opened for a session that has already started.");

            session.OpenBooking();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
