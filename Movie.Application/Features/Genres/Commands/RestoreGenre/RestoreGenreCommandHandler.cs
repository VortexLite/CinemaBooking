using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Movie.Domain.Entities.Genre;
using Shared.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Genres.Commands.RestoreGenre
{
    public sealed class RestoreGenreCommandHandler
        : IRequestHandler<RestoreGenreCommand>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RestoreGenreCommandHandler(
            IGenreRepository genreRepository, 
            IUnitOfWork unitOfWork)
        {
            _genreRepository = genreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RestoreGenreCommand request, 
            CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.GetByIdIncludingDeletedAsync(
                request.Id, cancellationToken);

            if (genre is null)
                throw new NotFoundException(nameof(Genre), request.Id);

            genre.Restore();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
