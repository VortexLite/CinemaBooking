using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Movie.Domain.Entities.Session;
using Shared.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Sessions.Commands.StartSession
{
    public sealed class StartSessionCommandHandler
        : IRequestHandler<StartSessionCommand>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TimeProvider _timeProvider;

        public StartSessionCommandHandler(ISessionRepository sessionRepository, 
            IUnitOfWork unitOfWork,
            TimeProvider timeProvider)
        {
            _sessionRepository = sessionRepository;
            _unitOfWork = unitOfWork;
            _timeProvider = timeProvider;
        }

        public async Task Handle(StartSessionCommand request, 
            CancellationToken cancellationToken)
        {
            var session = await _sessionRepository.GetByIdAsync(request.Id,
                cancellationToken);

            if (session is null)
                throw new NotFoundException(nameof(Session), request.Id);

            var utcNow = _timeProvider.GetUtcNow().UtcDateTime;

            if (session.StartTime > utcNow)
                throw new BusinessRuleException("Session cannot be started before its scheduled start time.");

            session.Start();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
