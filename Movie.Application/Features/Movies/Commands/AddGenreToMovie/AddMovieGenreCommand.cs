using MediatR;

namespace Movie.Application.Features.Movies.Commands.AddGenreToMovie
{
    public sealed record AddMovieGenreCommand(
        Guid MovieId,
        Guid GenreId)
        : IRequest;
}
