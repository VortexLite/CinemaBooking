using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.BackgroundJobs.Sessions
{
    public sealed class SessionLifecycleOptions
    {
        public const string SectionName =
            "SessionLifecycle";

        public int IntervalSeconds { get; init; } = 30;

        public int BatchSize { get; init; } = 100;
    }
}
