using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Halls.Commands.DeleteHall
{
    public sealed record DeleteHallCommand(Guid Id)
        : IRequest;
}
