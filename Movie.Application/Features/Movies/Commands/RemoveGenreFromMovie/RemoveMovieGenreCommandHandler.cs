using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Shared.Common.Exceptions;

namespace Movie.Application.Features.Movies.Commands.RemoveGenreFromMovie
{
    public class RemoveMovieGenreCommandHandler
        : IRequestHandler<RemoveMovieGenreCommand>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveMovieGenreCommandHandler(IMovieRepository movieRepository, 
            IUnitOfWork unitOfWork)
        {
            _movieRepository = movieRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RemoveMovieGenreCommand request,
            CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetWithGenresAsync(request.MovieId, cancellationToken);

            if (movie is null)
                throw new NotFoundException(nameof(Domain.Entities.Movie.Movie), request.MovieId);

            movie.RemoveGenre(request.GenreId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
