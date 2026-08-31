using Microsoft.EntityFrameworkCore;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Entities.Hall;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Repositories
{
    public sealed class HallRepository
        : BaseRepository<Hall>,
        IHallRepository
    {
        public HallRepository(MovieDbContext context)
            : base(context) { }

        public async Task<Hall?> GetWithSeatsAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Halls
                .Include(x => x.Seats)
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<Hall?> GetWithSeatsAndSessionsAsync(
            Guid id, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Halls
                .Include(x => x.Seats)
                .Include(x => x.Sessions)
                .FirstOrDefaultAsync(x => x.Id == id,
                    cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(
            string name, 
            CancellationToken cancellationToken = default)
        {
            string normalizedName = name.Trim();

            return await _context.Halls
                .IgnoreQueryFilters()
                 .AnyAsync(x => EF.Functions.ILike(
                     x.Name,
                     normalizedName),
                 cancellationToken);
        }

        public async Task<Hall?> GetByIdIncludingDeletedAsync(Guid id, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Halls
                .IgnoreQueryFilters()
                .Include(x => x.Seats)
                .FirstOrDefaultAsync(x => x.Id == id,
                    cancellationToken);
        }

        public async Task<bool> HasFutureSessionsAsync(Guid hallId, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Sessions
                .AnyAsync(x =>
                    x.HallId == hallId &&
                    x.StartTime > DateTime.UtcNow,
                    cancellationToken);
        }
    }
}
