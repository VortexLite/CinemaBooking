using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Common.Interfaces
{
    public interface ICacheLockProvider
    {
        ValueTask<ICacheLock> LockAsync(
            string key,
            CancellationToken cancellationToken = default);
    }
}
