using MediatR;

namespace Movie.Application.Features.Sessions.Commands.OpenSessionBooking
{
    public sealed record OpenSessionBookingCommand(Guid Id)
        : IRequest;
}
