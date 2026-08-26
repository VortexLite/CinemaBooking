using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.Application.Abstractions.Interfaces;
using Movie.Application.Caching;
using Movie.Application.Common.Interfaces;
using Movie.Application.Features.Movies.Queries.GetMovie;
using Movie.Application.Features.Movies.Queries.GetMovies;
using Movie.Domain.Enums;
using Shared.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Queries
{
    public sealed class MovieQueries : BaseQuery, IMovieQueries
    {
        private readonly ICacheService _cache;

        public MovieQueries(MovieDbContext context,
            ICacheService cache) 
            : base(context) 
        {
            _cache = cache;
        }

        public async Task<GetMovieResponse?> GetMovieAsync(
        Guid id,
        CancellationToken cancellationToken = default)
        {
            string cacheKey = CacheKeys.Movie(id);

            var cached = await _cache.GetAsync<GetMovieResponse>(
                cacheKey,
                cancellationToken);

            if (cached is not null)
                return cached;

            var movie = await _context.Movies
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ProjectToType<GetMovieResponse>()
                .FirstOrDefaultAsync(cancellationToken);

            if (movie is not null)
            {
                await _cache.SetAsync(
                    cacheKey,
                    movie,
                    TimeSpan.FromMinutes(10),
                    tags: new[]
                    {
                        CacheTags.Movies
                    },
                    cancellationToken);
            }

            return movie;
        }

        public async Task<PagedResult<GetMoviesResponse>> GetMoviesAsync(
            GetMoviesQuery request,
            CancellationToken cancellationToken = default)
        {
            string cacheKey = CacheKeys.Movies(
                request.Search,
                request.Status,
                request.GenreId,
                request.LanguageId,
                request.IncludeDeleted,
                request.Page,
                request.PageSize);

            var cached = await _cache.GetAsync<PagedResult<GetMoviesResponse>>(
                cacheKey,
                cancellationToken);

            if (cached is not null)
                return cached;

            IQueryable<Domain.Entities.Movie.Movie> query =
                _context.Movies.AsNoTracking();

            if (request.IncludeDeleted)
            {
                query = query.IgnoreQueryFilters();
            }

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x =>
                    EF.Functions.ILike(
                        x.Title,
                        $"%{request.Search}%"));
            }

            if (request.Status.HasValue)
            {
                query = query.Where(x =>
                    x.Status == request.Status.Value);
            }

            if (request.LanguageId.HasValue)
            {
                query = query.Where(x =>
                    x.LanguageId == request.LanguageId.Value);
            }

            if (request.GenreId.HasValue)
            {
                query = query.Where(x =>
                    x.MovieGenres.Any(g =>
                        g.GenreId == request.GenreId.Value));
            }

            int totalCount = await query.CountAsync(cancellationToken);

            List<GetMoviesResponse> items = await query
                .OrderBy(x => x.Title)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectToType<GetMoviesResponse>()
                .ToListAsync(cancellationToken);

            var result = new PagedResult<GetMoviesResponse>
            {
                Items = items,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };

            await _cache.SetAsync(
                cacheKey,
                result,
                TimeSpan.FromMinutes(5),
                tags: new[]
                {
                    CacheTags.Movies
                },
                cancellationToken);

            return result;
        }
    }
}
