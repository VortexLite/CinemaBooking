using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Movies.Queries.GetMovie
{
    public sealed class GetMovieResponse
    {
        public Guid Id { get; init; }

        public string Title { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

        public int DurationMinutes { get; init; }

        public byte AgeRestriction { get; init; }

        public string PosterUrl { get; init; } = string.Empty;

        public string? TrailerUrl { get; init; }

        public DateOnly ReleaseDate { get; init; }

        public string Status { get; init; } = string.Empty;

        public Guid LanguageId { get; init; }

        public string LanguageName { get; init; } = string.Empty;

        public IReadOnlyList<string> Genres { get; init; } = [];
    }
}
