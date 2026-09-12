using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Sessions.Commands.DeleteSession
{
    public sealed record DeleteSessionCommand(Guid Id)
        : IRequest;
}
