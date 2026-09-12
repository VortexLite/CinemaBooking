using MediatR;

namespace Movie.Application.Features.Halls.Commands.DeleteHall
{
    public sealed record DeleteHallCommand(Guid Id)
        : IRequest;
}
