using MediatR;

namespace Movie.Application.Features.Genres.Commands.CreateGenre
{
    public sealed record CreateGenreCommand(string Name)
        : IRequest<Guid>;
}
