using Mapster;
using Microsoft.EntityFrameworkCore;
using Movie.Application.Abstractions.Interfaces;
using Movie.Application.Features.Movies.Queries.GetMovie;
using Movie.Domain.Enums;
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
                    == MovieStatus.Published)
                .OrderBy(x => x.Title)
                .ToListAsync(cancellationToken);
        }

        public async Task<Domain.Entities.Movie.Movie?> GetDeletedByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Movies
                .IgnoreQueryFilters()
                .Include(x => x.MovieGenres)
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<List<Domain.Entities.Movie.Movie>> GetDeletedAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Movies
                .IgnoreQueryFilters()
                .Where(x => x.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> HasActiveOrFutureSessionsAsync(Guid movieId, DateTime utcNow,
            CancellationToken cancellationToken = default)
        {
            return await _context.Sessions
                .AnyAsync(x => 
                    x.MovieId == movieId && 
                    x.EndTime > utcNow &&
                    x.Status != SessionStatus.Cancelled &&
                    x.Status != SessionStatus.Finished,
                cancellationToken);
        }

        public async Task<Domain.Entities.Movie.Movie?> GetWithGenresAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Movies
                .Include(x => x.MovieGenres)
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }
    }
}
