using MediatR;
using Movie.Domain.Enums;

namespace Movie.Application.Features.Halls.Commands.ChangeSeatType
{
    public sealed record ChangeSeatTypeCommand(
        Guid HallId,
        Guid SeatId,
        SeatType Type)
        : IRequest;
}
