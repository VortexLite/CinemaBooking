using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Movie.Domain.Entities.Genre;
using Shared.Common.Exceptions;

namespace Movie.Application.Features.Genres.Commands.CreateGenre
{
    public sealed class CreateGenreCommandHandler
        : IRequestHandler<CreateGenreCommand, Guid>
    {
        public readonly IGenreRepository _genreRepository;
        public readonly IUnitOfWork _unitOfWork;

        public CreateGenreCommandHandler(
            IGenreRepository genreRepository,
            IUnitOfWork unitOfWork)
        {
            _genreRepository = genreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateGenreCommand request, 
            CancellationToken cancellationToken)
        {
            if (await _genreRepository.ExistsByNameAsync(request.Name, cancellationToken))
                throw new BusinessRuleException($"Genre '{request.Name}' already exists.");

            var genre = new Genre(request.Name);

            await _genreRepository.AddAsync(genre, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return genre.Id;
        }
    }
}
