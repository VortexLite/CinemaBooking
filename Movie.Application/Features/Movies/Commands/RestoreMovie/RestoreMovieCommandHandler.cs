using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Movie.Domain.Enums;
using Shared.Common.Exceptions;

namespace Movie.Application.Features.Movies.Commands.RestoreMovie
{
    public sealed class RestoreMovieCommandHandler
        : IRequestHandler<RestoreMovieCommand>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RestoreMovieCommandHandler(IMovieRepository movieRepository,
            ILanguageRepository languageRepository, 
            IGenreRepository genreRepository, 
            IUnitOfWork unitOfWork)
        {
            _movieRepository = movieRepository;
            _languageRepository = languageRepository;
            _genreRepository = genreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(
            RestoreMovieCommand request, 
            CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetDeletedByIdAsync(
                request.Id,
                cancellationToken);

            if (movie is null)
                throw new NotFoundException(nameof(Movie.Domain.Entities.Movie.Movie), request.Id);

            if (!movie.IsDeleted)
                return;

            var languageExists = await _languageRepository.ExistsAsync(movie.LanguageId,
                cancellationToken);

            if (!languageExists)
                throw new BusinessRuleException("Movie cannot be restored because its language is unavailable.");

            Guid[] genreIds = movie.MovieGenres
                .Select(x => x.GenreId)
                .Distinct()
                .ToArray();

            if (movie.Status is MovieStatus.Published or 
                MovieStatus.Archived && genreIds.Length == 0)
                throw new BusinessRuleException("Published or archived movie cannot be restored without genres.");

            var genreExist = await _genreRepository.AllExistAsync(genreIds , cancellationToken);

            if (!genreExist)
                throw new BusinessRuleException("Movie cannot be restored because one or more genres are unavailable.");

            movie.Restore();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
