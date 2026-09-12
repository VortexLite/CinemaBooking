using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Sessions.Commands.StartSession
{
    public sealed record StartSessionCommand(Guid Id)
        : IRequest;
}
