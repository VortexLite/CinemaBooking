using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Movie.Domain.Entities.Hall;
using Shared.Common.Exceptions;

namespace Movie.Application.Features.Halls.Commands.CreateHall
{
    public sealed class CreateHallCommandHandler
        : IRequestHandler<CreateHallCommand, Guid>
    {
        private readonly IHallRepository _hallRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateHallCommandHandler(
            IHallRepository hallRepository, 
            IUnitOfWork unitOfWork)
        {
            _hallRepository = hallRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateHallCommand request, 
            CancellationToken cancellationToken)
        {
            if (await _hallRepository.ExistsByNameAsync(request.Name, cancellationToken))
                throw new BusinessRuleException($"Hall '{request.Name}' already exists.");

            var hall = new Hall(
                request.Name,
                request.Rows,
                request.Columns);

            await _hallRepository.AddAsync(hall, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return hall.Id;
        }
    }
}
