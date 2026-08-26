using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Caching
{
    public static class CacheTags
    {
        public const string Movies = "movies";

        public const string Genres = "genres";

        public const string Languages = "languages";

        public const string Halls = "halls";

        public const string Sessions = "sessions";

        public static string Movie(Guid id)
            => $"movie:{id}";

        public static string Genre(Guid id)
            => $"genre:{id}";

        public static string Language(Guid id)
            => $"language:{id}";

        public static string Hall(Guid id)
            => $"hall:{id}";

        public static string Session(Guid id)
            => $"session:{id}";
    }
}
