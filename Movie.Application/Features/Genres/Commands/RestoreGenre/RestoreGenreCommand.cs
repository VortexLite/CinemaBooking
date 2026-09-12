using MediatR;

namespace Movie.Application.Features.Genres.Commands.RestoreGenre
{
    public sealed record RestoreGenreCommand(Guid Id)
        : IRequest;
}
