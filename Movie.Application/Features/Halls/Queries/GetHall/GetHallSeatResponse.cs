using Movie.Domain.Enums;

namespace Movie.Application.Features.Halls.Queries.GetHall
{
    public sealed class GetHallSeatResponse
    {
        public Guid Id { get; init; }

        public ushort Row { get; init; }

        public ushort Number { get; init; }

        public SeatType Type { get; init; }
    }
}
