using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Halls.Commands.UpdateHall
{
    public sealed record UpdateHallCommand(
        Guid Id,
        string Name,
        ushort Rows,
        ushort Columns)
        : IRequest;
}
