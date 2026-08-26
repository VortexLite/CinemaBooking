using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Application.Caching;
using Movie.Domain.Common;
using Shared.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Movies.Commands.PublishMovie
{
    public sealed class PublishMovieCommandHandler
        : IRequestHandler<PublishMovieCommand>
    {

        private readonly IMovieRepository _movieRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cache;

        public PublishMovieCommandHandler(IMovieRepository movieRepository, 
            IUnitOfWork unitOfWork, 
            ICacheService cache)
        {
            _movieRepository = movieRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task Handle(PublishMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetByIdAsync(
                request.Id,
                cancellationToken);

            if (movie is null)
                throw new NotFoundException(nameof(
                    Movie.Domain.Entities.Movie.Movie), 
                    request.Id);

            movie.Publish();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _cache.RemoveByPrefixAsync(
                CacheKeys.MoviesPrefix,
                cancellationToken);
        }
    }
}
