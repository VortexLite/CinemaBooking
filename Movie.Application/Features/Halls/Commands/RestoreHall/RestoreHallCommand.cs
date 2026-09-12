using MediatR;

namespace Movie.Application.Features.Halls.Commands.RestoreHall
{
    public sealed record RestoreHallCommand(Guid Id)
        : IRequest;
}
