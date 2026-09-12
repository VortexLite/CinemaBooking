using MediatR;

namespace Movie.Application.Features.Sessions.Commands.FinishSession
{
    public sealed record FinishSessionCommand(Guid Id)
        : IRequest;
}
