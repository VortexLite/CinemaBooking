using MediatR;

namespace Movie.Application.Features.Sessions.Commands.RestoreSession
{
    public sealed record RestoreSessionCommand(Guid Id)
        : IRequest;
}
