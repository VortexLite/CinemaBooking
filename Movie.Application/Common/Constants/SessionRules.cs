using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Common.Constants
{
    public static class SessionRules
    {
        public static readonly TimeSpan CleaningBuffer = TimeSpan.FromMinutes(15);
    }
}
