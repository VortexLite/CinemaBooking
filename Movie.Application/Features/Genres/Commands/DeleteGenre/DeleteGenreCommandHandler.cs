using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Movie.Domain.Entities.Genre;
using Shared.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Genres.Commands.DeleteGenre
{
    public class DeleteGenreCommandHandler
        : IRequestHandler<DeleteGenreCommand>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteGenreCommandHandler(
            IGenreRepository genreRepository,
            IUnitOfWork unitOfWork)
        {
            _genreRepository = genreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteGenreCommand request,
            CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.GetByIdAsync(request.Id,
                cancellationToken);

            if (genre is null)
                throw new NotFoundException(nameof(Genre), request.Id);

            if (await _genreRepository.IsUsedByProtectedMoviesAsync(request.Id, cancellationToken))
                throw new ConflictException("Genre cannot be deleted because it is used by published movies.");

            genre.Delete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
