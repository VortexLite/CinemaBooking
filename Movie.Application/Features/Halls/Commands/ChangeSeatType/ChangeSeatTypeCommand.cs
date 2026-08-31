using MediatR;
using Movie.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Halls.Commands.ChangeSeatType
{
    public sealed record ChangeSeatTypeCommand(
        Guid HallId,
        Guid SeatId,
        SeatType Type)
        : IRequest;
}
