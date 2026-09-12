using MediatR;

namespace Movie.Application.Features.Movies.Commands.RestoreMovie
{
    public sealed record RestoreMovieCommand(Guid Id)
        : IRequest;
}
