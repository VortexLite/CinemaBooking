using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Common
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}
