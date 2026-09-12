using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Sessions.Commands.CancelSession
{
    public sealed record CancelSessionCommand(
        Guid Id,
        string Reason)
        : IRequest;
}
