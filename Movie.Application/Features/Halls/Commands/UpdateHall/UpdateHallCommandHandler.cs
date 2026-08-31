using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Movie.Domain.Exceptions;
using Shared.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Halls.Commands.UpdateHall
{
    public sealed class UpdateHallCommandHandler
        : IRequestHandler<UpdateHallCommand>
    {
        private readonly IHallRepository _hallRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateHallCommandHandler(
            IHallRepository hallRepository, 
            IUnitOfWork unitOfWork)
        {
            _hallRepository = hallRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateHallCommand request,
            CancellationToken cancellationToken)
        {
            var hall = await _hallRepository.GetByIdAsync(request.Id,
                cancellationToken);

            if (hall is null)
                throw new NotFoundException(nameof(Domain.Entities.Hall.Hall),
                request.Id);

            var layoutChanged = hall.Rows != request.Rows ||
                hall.Columns != request.Columns;

            if (layoutChanged &&
                await _hallRepository.HasFutureSessionsAsync(hall.Id,
                    cancellationToken))
                throw new DomainException("Hall layout cannot be changed while future sessions exist.");

            var nameExists = await _hallRepository.ExistsByNameAsync(request.Name,
                cancellationToken);

            if (nameExists &&
                !string.Equals(
                    hall.Name,
                    request.Name.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                throw new DomainException($"Hall '{request.Name}' already exists.");

            hall.Update(
                request.Name,
                request.Rows,
                request.Columns);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
