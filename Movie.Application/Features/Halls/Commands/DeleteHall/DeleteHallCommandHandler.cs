using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Movie.Domain.Exceptions;
using Shared.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Halls.Commands.DeleteHall
{
    public sealed class DeleteHallCommandHandler
        : IRequestHandler<DeleteHallCommand>
    {
        private readonly IHallRepository _hallRepository;
        private readonly TimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteHallCommandHandler(
            IHallRepository hallRepository, 
            TimeProvider timeProvider,
            IUnitOfWork unitOfWork)
        {
            _hallRepository = hallRepository;
            _timeProvider = timeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteHallCommand request,
            CancellationToken cancellationToken)
        {
            var hall = await _hallRepository.GetByIdAsync(request.Id,
                cancellationToken);

            if (hall is null)
                throw new NotFoundException(nameof(Domain.Entities.Hall.Hall), request.Id);

            var utcNow = _timeProvider.GetUtcNow().UtcDateTime;

            if (await _hallRepository.HasFutureSessionsAsync(hall.Id, utcNow, cancellationToken))
                throw new DomainException("Hall cannot be deleted while future sessions exist.");

            hall.Delete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
