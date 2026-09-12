using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Shared.Common.Exceptions;

namespace Movie.Application.Features.Movies.Commands.CreateMovie
{
    public sealed class CreateMovieCommandHandler
        : IRequestHandler<CreateMovieCommand, Guid>
    {
        private readonly IMovieRepository _repository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMovieCommandHandler(IMovieRepository repository, 
            ILanguageRepository languageRepository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateMovieCommand request, 
            CancellationToken cancellationToken)
        {
            var language = await _languageRepository.GetByIdAsync(request.LanguageId, cancellationToken);

            if (language is null)
                throw new NotFoundException(nameof(Domain.Entities.Language.Language));

            var movie = new Domain.Entities.Movie.Movie(
                request.Title,
                request.Description,
                request.DurationMinutes,
                request.AgeRestriction,
                request.PosterUrl,
                request.TrailerUrl,
                request.ReleaseDate,
                request.LanguageId);

            await _repository.AddAsync(movie, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return movie.Id;
        }
    }
}
