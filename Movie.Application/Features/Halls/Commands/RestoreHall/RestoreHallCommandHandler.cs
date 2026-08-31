using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Movie.Domain.Entities.Hall;
using Shared.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Halls.Commands.RestoreHall
{
    public sealed class RestoreHallCommandHandler
        : IRequestHandler<RestoreHallCommand>
    {
        private readonly IHallRepository _hallRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RestoreHallCommandHandler(
            IHallRepository hallRepository, 
            IUnitOfWork unitOfWork)
        {
            _hallRepository = hallRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RestoreHallCommand request,
            CancellationToken cancellationToken)
        {
            var hall = await _hallRepository.GetByIdIncludingDeletedAsync(request.Id,
                cancellationToken);

            if (hall is null)
                throw new NotFoundException(nameof(Hall), request.Id);

            hall.Restore();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
