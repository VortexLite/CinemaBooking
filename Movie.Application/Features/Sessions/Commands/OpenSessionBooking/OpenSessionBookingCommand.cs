using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Sessions.Commands.OpenSessionBooking
{
    public sealed record OpenSessionBookingCommand(Guid Id)
        : IRequest;
}
