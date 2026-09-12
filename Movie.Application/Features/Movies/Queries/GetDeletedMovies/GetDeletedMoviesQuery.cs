using MediatR;
using Movie.Application.Caching;
using Movie.Application.Common.Interfaces;
using Movie.Application.Features.Movies.Queries.GetMovies;
using Shared.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Movies.Queries.GetDeletedMovies
{
    public sealed record GetDeletedMoviesQuery(
        int Page,
        int PageSize)
        : ICachedQuery<PagedResult<GetMoviesResponse>>
    {
        public string CacheKey => CacheKeys.MoviesDeleted(Page, PageSize);

        public TimeSpan? Expiration => TimeSpan.FromMinutes(5);

        public IEnumerable<string>? Tags =>
        [
            CacheTags.Movies
        ];
    }
}
