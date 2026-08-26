using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Shared.Common.Exceptions;

namespace Movie.Application.Features.Movies.Commands.ArchiveMovie
{
    public sealed class ArchiveMovieCommandHandler
        : IRequestHandler<ArchiveMovieCommand>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ArchiveMovieCommandHandler(IMovieRepository movieRepository, 
            IUnitOfWork unitOfWork)
        {
            _movieRepository = movieRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ArchiveMovieCommand request, 
            CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetByIdAsync(
                request.Id, 
                cancellationToken);

            if (movie is null)
                throw new NotFoundException(nameof(Movie.Domain.Entities.Movie.Movie), request.Id);

            movie.Archive();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
