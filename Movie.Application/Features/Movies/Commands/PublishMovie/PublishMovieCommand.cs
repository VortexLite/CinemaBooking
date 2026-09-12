using MediatR;

namespace Movie.Application.Features.Movies.Commands.PublishMovie
{
    public sealed record PublishMovieCommand(Guid Id)
        : IRequest;
}
