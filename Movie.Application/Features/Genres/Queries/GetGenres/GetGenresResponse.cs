using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Genres.Queries.GetGenres
{
    public sealed class GetGenresResponse
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = string.Empty;
    }
}
