using Movie.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Entities.Seat
{
    public sealed partial class Seat
    {
        public void ChangeType(SeatType seatType)
        {
            if (SeatType == seatType)
                return;

            SeatType = seatType;
        }
    }
}
