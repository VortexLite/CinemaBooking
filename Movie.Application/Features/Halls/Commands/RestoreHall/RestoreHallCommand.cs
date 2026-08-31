using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Halls.Commands.RestoreHall
{
    public sealed record RestoreHallCommand(Guid Id)
        : IRequest;
}
