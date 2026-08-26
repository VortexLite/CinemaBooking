using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Genres.Queries.GetGenre
{
    public sealed class GetGenreResponse
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = string.Empty;
    }
}
