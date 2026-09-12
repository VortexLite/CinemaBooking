using Movie.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Caching
{
    public static class CacheKeys
    {
        #region Prefixes

        public const string MoviePrefix = "movie:";
        public const string MoviesPrefix = "movies:";

        public const string GenrePrefix = "genre:";
        public const string GenresPrefix = "genres:";

        public const string LanguagePrefix = "language:";
        public const string LanguagesPrefix = "languages:";

        public const string HallPrefix = "hall:";
        public const string HallsPrefix = "halls:";

        public const string SessionPrefix = "session:";
        public const string SessionsPrefix = "sessions:";

        #endregion

        #region Movies

        public static string Movie(Guid id)
            => $"{MoviePrefix}{id}";

        public static string Movies(
            string? search,
            MovieStatus? status,
            Guid? genreId,
            Guid? languageId,
            bool includeDeleted,
            int page,
            int pageSize)
        {
            return $"{MoviesPrefix}" +
                   $"search:{search ?? "all"}:" +
                   $"status:{status?.ToString() ?? "all"}:" +
                   $"genre:{genreId?.ToString() ?? "all"}:" +
                   $"language:{languageId?.ToString() ?? "all"}:" +
                   $"deleted:{includeDeleted}:" +
                   $"page:{page}:" +
                   $"size:{pageSize}";
        }

        #endregion

        #region MoviesDeleted

        public static string MoviesDeleted(
            int page,
            int pageSize)
        {
            return $"{MoviesPrefix}:deleted:" +
                   $"page:{page}:" +
                   $"size:{pageSize}";
        }

        #endregion

        #region Genres

        public static string Genre(Guid id)
            => $"{GenrePrefix}{id}";

        public static string Genres(bool includeDeleted)
        {
            return $"{GenresPrefix}" +
                   $"deleted:{includeDeleted}";
        }

        #endregion

        #region Languages

        public static string Language(Guid id)
            => $"{LanguagePrefix}{id}";

        public static string Languages(bool includeDeleted) 
            => $"{LanguagesPrefix}deleted:{includeDeleted}";

        #endregion

        #region Halls

        public static string Hall(Guid id)
            => $"{HallPrefix}{id}";

        public static string Halls(bool includeDeleted)
            => $"{HallsPrefix}deleted:{includeDeleted}";

        #endregion

        #region Sessions

        public static string Session(Guid id)
            => $"{SessionPrefix}{id}";

        public static string Sessions(bool includeDeleted)
            => $"{SessionsPrefix}deleted:{includeDeleted}";

        public static string Sessions(
            DateTime? fromUtc,
            DateTime? toUtc,
            Guid? movieId,
            Guid? hallId,
            SessionStatus? status,
            bool includeDeleted)
        {
            return
                $"{SessionsPrefix}" +
                $"from={fromUtc?.ToUniversalTime():O};" +
                $"to={toUtc?.ToUniversalTime():O};" +
                $"movie={movieId};" +
                $"hall={hallId};" +
                $"status:{status?.ToString() ?? "all"}:" +
                $"deleted={includeDeleted}";
        }

        #endregion
    }
}