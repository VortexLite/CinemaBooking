using MediatR;

namespace Movie.Application.Features.Genres.Commands.UpdateGenre
{
    public sealed record UpdateGenreCommand(Guid Id, string Name)
        : IRequest;
}
