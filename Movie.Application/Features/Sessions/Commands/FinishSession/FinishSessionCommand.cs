using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Sessions.Commands.FinishSession
{
    public sealed record FinishSessionCommand(Guid Id)
        : IRequest;
}
