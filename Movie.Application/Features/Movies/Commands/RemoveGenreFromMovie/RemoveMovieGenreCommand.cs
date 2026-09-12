using MediatR;

namespace Movie.Application.Features.Movies.Commands.RemoveGenreFromMovie
{
    public sealed record RemoveMovieGenreCommand(
        Guid MovieId,
        Guid GenreId)
        : IRequest;
}
