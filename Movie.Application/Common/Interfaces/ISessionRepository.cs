using Movie.Domain.Entities.Session;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Abstractions.Interfaces
{
    public interface ISessionRepository
        : IBaseRepository<Session>
    {
        Task<Session?> GetDetailsAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<List<Session>> GetMovieSessionsAsync(
            Guid movieId,
            CancellationToken cancellationToken = default);

        Task<List<Session>> GetHallSessionsAsync(
            Guid hallId,
            CancellationToken cancellationToken = default);

        Task<List<Session>> GetUpcomingAsync(
            CancellationToken cancellationToken = default);

        Task<List<Session>> GetByDateAsync(
            DateOnly date,
            CancellationToken cancellationToken = default);

        Task<bool> IsHallBusyAsync(
            Guid hallId,
            DateTime start,
            DateTime end,
            CancellationToken cancellationToken = default);

        Task<Session?> GetForBookingAsync(
            Guid sessionId,
            CancellationToken cancellationToken = default);
    }
}
