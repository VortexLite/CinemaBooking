using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Movie.Domain.Entities.Genre;
using Shared.Common.Exceptions;

namespace Movie.Application.Features.Genres.Commands.UpdateGenre
{
    public sealed class UpdateGenreCommandHandler
        : IRequestHandler<UpdateGenreCommand>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateGenreCommandHandler(
            IGenreRepository genreRepository, 
            IUnitOfWork unitOfWork)
        {
            _genreRepository = genreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateGenreCommand request, 
            CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.GetByIdAsync(request.Id, cancellationToken);

            if (genre is null)
                throw new NotFoundException(nameof(Genre), request.Id);

            var exists = await _genreRepository.ExistsByNameAsync(request.Name, cancellationToken);

            if (exists && !string.Equals(
                genre.Name,
                request.Name.Trim(),
                StringComparison.OrdinalIgnoreCase))
                throw new BusinessRuleException($"Genre '{request.Name}' already exists.");

            genre.Update(request.Name);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
