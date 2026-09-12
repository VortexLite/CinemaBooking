using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Shared.Common.Exceptions;

namespace Movie.Application.Features.Movies.Commands.PublishMovie
{
    public sealed class PublishMovieCommandHandler
        : IRequestHandler<PublishMovieCommand>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PublishMovieCommandHandler(IMovieRepository movieRepository, 
            IUnitOfWork unitOfWork)
        {
            _movieRepository = movieRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(PublishMovieCommand request, 
            CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetWithGenresAsync(request.Id, cancellationToken);

            if (movie is null)
                throw new NotFoundException(nameof(Domain.Entities.Movie.Movie), request.Id);

            movie.Publish();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
