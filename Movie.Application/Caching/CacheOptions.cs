using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Caching
{
    public static class CacheOptions
    {
        public static readonly TimeSpan Movie =
            TimeSpan.FromMinutes(10);

        public static readonly TimeSpan Movies =
            TimeSpan.FromMinutes(5);

        public static readonly TimeSpan Genres =
            TimeSpan.FromHours(1);

        public static readonly TimeSpan Languages =
            TimeSpan.FromHours(12);

        public static readonly TimeSpan PublishedMovies =
            TimeSpan.FromMinutes(5);
    }
}
