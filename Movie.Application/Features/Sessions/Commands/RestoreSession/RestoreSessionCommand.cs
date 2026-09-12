using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Sessions.Commands.RestoreSession
{
    public sealed record RestoreSessionCommand(Guid Id)
        : IRequest;
}
