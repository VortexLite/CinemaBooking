using Microsoft.EntityFrameworkCore;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Entities.Genre;
using Movie.Domain.Enums;
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

        public async Task<bool> AllExistAsync(IReadOnlyCollection<Guid> ids,
            CancellationToken cancellationToken = default)
        {
            if (ids.Count == 0)
                return true;

            var distincIds = ids.Distinct().ToArray();

            var existingCount = await _context.Genres
                .CountAsync(x => distincIds.Contains(x.Id),
                    cancellationToken);

            return existingCount == distincIds.Length;
        }

        public async Task<bool> IsUsedByProtectedMoviesAsync(Guid genreId,
            CancellationToken cancellationToken = default)
        {
            return await _context.Movies
                .AnyAsync(
                    movie =>
                        movie.Status == MovieStatus.Published ||
                        movie.Status == MovieStatus.Archived &&
                        movie.MovieGenres.Any(
                            mg => mg.GenreId == genreId),
                    cancellationToken);
        }
    }
}
