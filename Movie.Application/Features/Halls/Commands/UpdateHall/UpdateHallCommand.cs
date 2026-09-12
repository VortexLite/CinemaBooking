using MediatR;

namespace Movie.Application.Features.Halls.Commands.UpdateHall
{
    public sealed record UpdateHallCommand(
        Guid Id,
        string Name,
        ushort Rows,
        ushort Columns)
        : IRequest;
}
