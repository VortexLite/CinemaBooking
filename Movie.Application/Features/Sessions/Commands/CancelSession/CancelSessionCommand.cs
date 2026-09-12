using MediatR;

namespace Movie.Application.Features.Sessions.Commands.CancelSession
{
    public sealed record CancelSessionCommand(Guid Id, string Reason)
        : IRequest;
}
