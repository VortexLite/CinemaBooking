using Mapster;
using Microsoft.EntityFrameworkCore;
using Movie.Application.Common.Interfaces;
using Movie.Application.Features.Genres.Queries.GetGenre;
using Movie.Application.Features.Genres.Queries.GetGenres;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Queries
{
    public sealed class GenreQueries
        : BaseQuery, IGenreQueries
    {
        public GenreQueries(MovieDbContext context) : base(context) { }

        public async Task<GetGenreResponse?> GetGenreAsync(Guid id, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Genres
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ProjectToType<GetGenreResponse>()
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<GetGenresResponse>> GetGenresAsync(bool includeDeleted, 
            CancellationToken cancellationToken = default)
        {
            var query = _context.Genres.AsNoTracking();

            if (includeDeleted)
                query = query.IgnoreQueryFilters();

            return await query
                .OrderBy(x => x.Name)
                .ProjectToType<GetGenresResponse>()
                .ToListAsync(cancellationToken);
        }
    }
}
