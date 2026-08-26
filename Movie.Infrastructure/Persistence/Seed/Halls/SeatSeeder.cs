using Microsoft.EntityFrameworkCore;
using Movie.Domain.Entities.Hall;
using Movie.Domain.Entities.Seat;
using Movie.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Seed.Halls
{
    public static class SeatSeeder
    {
        public static async Task SeedAsync(MovieDbContext context)
        {
            if (await context.Seats.AnyAsync())
                return;

            var halls = await context.Halls.ToListAsync();

            var seats = new List<Seat>();

            foreach (var hall in halls)
            {
                for (ushort row = 1; row <= hall.Rows; row++)
                {
                    for (ushort number = 1; number <= hall.Columns; number++)
                    {
                        SeatType seatType = SeatType.Standard;

                        if (row == hall.Rows)
                            seatType = SeatType.VIP;

                        seats.Add(new Seat(
                            hall.Id,
                            row,
                            number,
                            seatType));
                    }
                }
            }

            await context.Seats.AddRangeAsync(seats);

            await context.SaveChangesAsync();
        }
    }
}
