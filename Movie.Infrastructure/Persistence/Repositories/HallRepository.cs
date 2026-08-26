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

        public async Task<List<Hall>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Halls
                .AsNoTracking()
                 .OrderBy(x => x.Name)
                 .ToListAsync(cancellationToken);
        }

        public async Task<Hall?> GetWithSeatAndSessionAsync(
            Guid id, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Halls
                .Include(x => x.Seats)
                .Include(x => x.Sessions)
                .FirstOrDefaultAsync(x => x.Id == id,
                cancellationToken);
        }

        public async Task<bool> NameExistAsync(
            string name, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Halls
                 .AnyAsync(x => x.Name == name,
                 cancellationToken);
        }
    }
}
