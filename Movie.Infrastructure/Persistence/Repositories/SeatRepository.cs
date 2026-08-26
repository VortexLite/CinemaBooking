using Microsoft.EntityFrameworkCore;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Entities.Seat;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Repositories
{
    public sealed class SeatRepository
        : BaseRepository<Seat>,
        ISeatRepository
    {
        public SeatRepository(MovieDbContext context)
            : base(context) { }

        public async Task<List<Seat>> GetByHallAsync(
            Guid hallId, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Seats
                .AsNoTracking()
                .Where(x => x.HallId == hallId)
                .OrderBy(x => x.Row)
                    .ThenBy(x => x.Number)
                .ToListAsync(cancellationToken);
        }

        public async Task<Seat?> GetByPositionAsync(
            Guid hallId, 
            ushort row, 
            ushort number, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Seats
                .FirstOrDefaultAsync(
                    x => x.HallId == hallId &&
                    x.Number == number,
                    cancellationToken);
        }

        public async Task<bool> ExistsAsync(
            Guid hallId, 
            ushort row, 
            ushort number, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Seats
                .AnyAsync(
                    x => x.HallId == hallId &&
                    x.Row == row &&
                    x.Number == number,
                    cancellationToken);
        }

        public async Task<int> CountAsync(
            Guid hallId,
            CancellationToken cancellationToken = default)
        {
            return await _context.Seats
                .CountAsync(x => x.HallId == hallId, 
                cancellationToken);
        }
    }
}
