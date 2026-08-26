using Movie.Domain.Entities.Hall;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Abstractions.Interfaces
{
    public interface IHallRepository
        : IBaseRepository<Hall>
    {
        Task<Hall?> GetWithSeatAndSessionAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<List<Hall>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<bool> NameExistAsync(
            string name,
            CancellationToken cancellationToken= default);
    }
}
