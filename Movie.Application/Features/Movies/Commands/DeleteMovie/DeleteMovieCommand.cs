using MediatR;

namespace Movie.Application.Features.Movies.Commands.DeleteMovie
{
    public sealed record DeleteMovieCommand(Guid Id)
        : IRequest;
}
