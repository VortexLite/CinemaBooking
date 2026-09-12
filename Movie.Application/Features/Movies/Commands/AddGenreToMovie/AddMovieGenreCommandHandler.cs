using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using Movie.Domain.Entities.Genre;
using Shared.Common.Exceptions;

namespace Movie.Application.Features.Movies.Commands.AddGenreToMovie
{
    public sealed class AddMovieGenreCommandHandler
        : IRequestHandler<AddMovieGenreCommand>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddMovieGenreCommandHandler(IMovieRepository movieRepository, 
            IGenreRepository genreRepository, 
            IUnitOfWork unitOfWork)
        {
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AddMovieGenreCommand request, 
            CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetWithGenresAsync(request.MovieId, cancellationToken);

            if (movie is null)
                throw new NotFoundException(nameof(Domain.Entities.Movie.Movie), request.MovieId);

            var genre = await _genreRepository.GetByIdAsync(request.GenreId, cancellationToken);

            if (genre is null)
                throw new NotFoundException(nameof(Genre), request.GenreId);

            movie.AddGenre(genre);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
