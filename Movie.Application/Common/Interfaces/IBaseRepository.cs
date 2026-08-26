using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Abstractions.Interfaces
{
    public interface IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<TEntity?> GetByIdAsync(Guid id,
            CancellationToken cancellationToken = default);

        Task AddAsync(TEntity entity,
            CancellationToken cancellationToken = default);

        void Update(TEntity entity);

        void Remove(TEntity entity);
    }
}
