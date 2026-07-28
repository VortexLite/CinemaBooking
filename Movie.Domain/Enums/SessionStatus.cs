using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Enums
{
    public enum SessionStatus : byte
    {
        Scheduled = 1,

        OpenForBooking = 2,

        SoldOut = 3,

        Started = 4,

        Finished = 5,

        Cancelled = 6
    }
}
