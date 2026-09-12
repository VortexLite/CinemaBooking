using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Movie.Domain.Entities.Hall;
using Shared.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Halls.Commands.ChangeSeatType
{
    public sealed class ChangeSeatTypeCommandHandler
        : IRequestHandler<ChangeSeatTypeCommand>
    {
        private readonly IHallRepository _hallRepository;
        private readonly TimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeSeatTypeCommandHandler(
            IHallRepository hallRepository,
            TimeProvider timeProvider,
            IUnitOfWork unitOfWork)
        {
            _hallRepository = hallRepository;
            _timeProvider = timeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ChangeSeatTypeCommand request, 
            CancellationToken cancellationToken)
        {
            var hall = await _hallRepository.GetWithSeatsAsync(request.HallId,
                cancellationToken);

            if (hall is null)
                throw new NotFoundException(nameof(Hall),request.HallId);

            var utcNow = _timeProvider.GetUtcNow().UtcDateTime;

            if (await _hallRepository.HasFutureSessionsAsync(hall.Id, utcNow,
                    cancellationToken))
                throw new BusinessRuleException(
                    "Seat type cannot be changed while future sessions exist.");

            hall.ChangeSeatType(request.SeatId, request.Type);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
