using MediatR;

namespace Movie.Application.Features.Genres.Commands.DeleteGenre
{
    public sealed record DeleteGenreCommand(Guid Id)
        : IRequest;
}
