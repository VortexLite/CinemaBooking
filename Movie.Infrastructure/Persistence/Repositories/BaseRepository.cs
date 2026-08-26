using Microsoft.EntityFrameworkCore;
using Movie.Application.Abstractions.Interfaces;
using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Repositories
{
    public abstract class BaseRepository<TEntity>
        : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly MovieDbContext _context;

        protected BaseRepository(MovieDbContext context)
        {
            _context = context;
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>()
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public virtual async Task AddAsync(TEntity entity, 
            CancellationToken cancellationToken = default)
        {
            await _context.Set<TEntity>()
                .AddAsync(entity, cancellationToken);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>()
                .Update(entity);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>()
                .Remove(entity);
        }

        
    }
}
