using Microsoft.EntityFrameworkCore;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Entities.Genre;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Repositories
{
    public sealed class GenreRepository
        : BaseRepository<Genre>,
        IGenreRepository
    {
        public GenreRepository(MovieDbContext context)
            : base(context) { }

        public async Task<List<Genre>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Genres.AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<Genre?> GetByNameAsync(string name, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Genres.
                FirstOrDefaultAsync(x => x.Name == name,
                    cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(string name,
            CancellationToken cancellationToken = default)
        {
            return await _context.Genres
                .IgnoreQueryFilters()
                .AnyAsync(x => EF.Functions.ILike(
                    x.Name,
                    name.Trim()),
                    cancellationToken);
        }

        public async Task<bool> ExistAsync(Guid genreId, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Genres.AnyAsync(
                x => x.Id == genreId,
                cancellationToken);
        }

        public async Task<Genre?> GetByIdIncludingDeletedAsync(Guid id, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Genres
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == id,
                cancellationToken);
        }
    }
}
