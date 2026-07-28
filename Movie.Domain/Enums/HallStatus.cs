using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Enums
{
    public enum HallStatus : byte
    {
        Active = 1,

        Maintenance = 2,

        Closed = 3
    }
}
