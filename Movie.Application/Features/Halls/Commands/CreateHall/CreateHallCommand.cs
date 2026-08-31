using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Halls.Commands.CreateHall
{
    public sealed record CreateHallCommand(
        string Name,
        ushort Rows,
        ushort Columns)
        : IRequest<Guid>;
}
