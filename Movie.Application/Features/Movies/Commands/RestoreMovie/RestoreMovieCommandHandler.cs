using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Shared.Common.Exceptions;

namespace Movie.Application.Features.Movies.Commands.RestoreMovie
{
    public sealed class RestoreMovieCommandHandler
        : IRequestHandler<RestoreMovieCommand>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RestoreMovieCommandHandler(IMovieRepository movieRepository, 
            IUnitOfWork unitOfWork)
        {
            _movieRepository = movieRepository;
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

            movie.Restore();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
