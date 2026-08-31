using Movie.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

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
