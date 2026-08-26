using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Application.Caching;
using Movie.Domain.Common;
using Shared.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Movies.Commands.UpdateMovie
{
    public sealed class UpdateMovieCommandHandler
        : IRequestHandler<UpdateMovieCommand>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMovieCommandHandler(
            IMovieRepository movieRepository,
            ILanguageRepository languageRepository,
            IUnitOfWork unitOfWork)
        {
            _movieRepository = movieRepository;
            _languageRepository = languageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(
            UpdateMovieCommand request,
            CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetByIdAsync(
                request.Id,
                cancellationToken);

            if (movie is null)
                throw new NotFoundException(nameof(Domain.Entities.Movie.Movie));

            if (!await _languageRepository.ExistsAsync(
                    request.LanguageId,
                    cancellationToken))
                throw new NotFoundException(nameof(Domain.Entities.Language.Language), request.LanguageId);

            movie.Update(
                request.Title,
                request.Description,
                request.DurationMinutes,
                request.AgeRestriction,
                request.PosterUrl,
                request.TrailerUrl,
                request.ReleaseDate,
                request.LanguageId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
