using Mapster;
using Microsoft.EntityFrameworkCore;
using Movie.Application.Abstractions.Interfaces;
using Movie.Application.Features.Movies.Queries.GetMovie;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Repositories
{
    public sealed class MovieRepository
        : BaseRepository<Domain.Entities.Movie.Movie>,
        IMovieRepository
    {
        public MovieRepository(MovieDbContext context)
            : base (context) { }

        public async Task<Domain.Entities.Movie.Movie?> GetDetailsAsync(
            Guid id, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Movies
                .Include(x => x.Language)
                .Include(x => x.MovieGenres)
                .ThenInclude(x => x.Genre)
                .Include(x => x.Sessions)
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<Domain.Entities.Movie.Movie?> GetByTitleAsync(
            string title, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Movies
                .FirstOrDefaultAsync(x => x.Title == title
                , cancellationToken);
        }

        public async Task<bool> ExistAsync(
            string title, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Movies
                .AnyAsync(
                    x => x.Title == title,
                    cancellationToken);
        }

        public async Task<List<Domain.Entities.Movie.Movie>> GetPublishAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Movies
                .AsNoTracking()
                .Where(x => x.Status
                    == Domain.Enums.MovieStatus.Published)
                .OrderBy(x => x.Title)
                .ToListAsync(cancellationToken);
        }

        public async Task<Domain.Entities.Movie.Movie?> GetDeletedByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Movies
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task RestoreAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            var movie = await _context.Movies
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);

            if (movie is null)
                return;

            movie.Restore();
        }

        public async Task<List<Domain.Entities.Movie.Movie>> GetDeletedAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Movies
                .IgnoreQueryFilters()
                .Where(x => x.IsDeleted)
                .ToListAsync(cancellationToken);
        }
    }
}
