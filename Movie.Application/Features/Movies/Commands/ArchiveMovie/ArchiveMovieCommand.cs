using MediatR;

namespace Movie.Application.Features.Movies.Commands.ArchiveMovie
{
    public sealed record ArchiveMovieCommand(Guid Id)
        : IRequest;
}
