using Microsoft.EntityFrameworkCore;
using Movie.Application.Common.Interfaces;
using Movie.Application.Features.Halls.Queries.GetHall;
using Movie.Application.Features.Halls.Queries.GetHalls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Queries
{
    public sealed class HallQueries
        : BaseQuery, IHallQueries
    {
        public HallQueries(MovieDbContext context) : base(context) { }

        public Task<GetHallResponse?> GetHallAsync(Guid id, 
            CancellationToken cancellationToken = default)
        {
            return _context.Halls
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new GetHallResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Rows = x.Rows,
                    Columns = x.Columns,
                    SeatsCount = x.Seats.Count,

                    Seats = x.Seats
                        .OrderBy(s => s.Row)
                        .ThenBy(s => s.Number)
                        .Select(s => new GetHallSeatResponse
                        {
                            Id = s.Id,
                            Row = s.Row,
                            Number = s.Number,
                            Type = s.SeatType
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task<List<GetHallsResponse>> GetHallsAsync(bool includeDeleted, 
            CancellationToken cancellationToken = default)
        {
            var query = _context.Halls.AsNoTracking();
            
            if (includeDeleted)
                query = query.IgnoreQueryFilters();

            return query
                .OrderBy(x => x.Name)
                .Select(x => new GetHallsResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Rows = x.Rows,
                    Columns = x.Columns,
                    SeatsCount = x.Seats.Count
                })
                .ToListAsync(cancellationToken);
        }
    }
}
