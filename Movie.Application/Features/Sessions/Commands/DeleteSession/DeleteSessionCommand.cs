using MediatR;

namespace Movie.Application.Features.Sessions.Commands.DeleteSession
{
    public sealed record DeleteSessionCommand(Guid Id)
        : IRequest;
}
