using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Movie.Domain.Entities.Session;
using Shared.Common.Exceptions;

namespace Movie.Application.Features.Sessions.Commands.DeleteSession
{
    public sealed class DeleteSessionCommandHandler
        : IRequestHandler<DeleteSessionCommand>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TimeProvider _timeProvider;

        public DeleteSessionCommandHandler(ISessionRepository sessionRepository, 
            IUnitOfWork unitOfWork, 
            TimeProvider timeProvider)
        {
            _sessionRepository = sessionRepository;
            _unitOfWork = unitOfWork;
            _timeProvider = timeProvider;
        }

        public async Task Handle(DeleteSessionCommand request, 
            CancellationToken cancellationToken)
        {
            var session = await _sessionRepository.GetByIdAsync(request.Id, cancellationToken);

            if (session is null)
                throw new NotFoundException(nameof(Session), request.Id);

            var utcNow = _timeProvider.GetUtcNow().UtcDateTime;

            if (session.StartTime <= utcNow)
                throw new BusinessRuleException("A started or completed session cannot be deleted.");

            session.Delete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
