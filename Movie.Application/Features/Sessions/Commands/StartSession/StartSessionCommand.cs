using MediatR;

namespace Movie.Application.Features.Sessions.Commands.StartSession
{
    public sealed record StartSessionCommand(Guid Id)
        : IRequest;
}
