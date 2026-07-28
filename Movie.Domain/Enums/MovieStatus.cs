using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Enums
{
    public enum MovieStatus : byte
    {
        Draft = 1,

        Published = 2,

        ComingSoon = 3,

        InCinema = 4,

        Archived = 5,

        Deleted = 6
    }
}
