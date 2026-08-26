using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Application.Caching;
using Movie.Domain.Common;
using Shared.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Movies.Commands.DeleteMovie
{
    public sealed class DeleteMovieCommandHandler
        : IRequestHandler<DeleteMovieCommand>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMovieCommandHandler(IMovieRepository movieRepository, 
            IUnitOfWork unitOfWork)
        {
            _movieRepository = movieRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteMovieCommand request, 
            CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetByIdAsync(
                request.Id,
                cancellationToken);

            if (movie is null)
                throw new NotFoundException(nameof(Domain.Entities.Movie.Movie), request.Id);

            _movieRepository.Remove(movie);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
