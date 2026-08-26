using Movie.Application.Common.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Caching
{
    public sealed class CacheLockProvider : ICacheLockProvider
    {
        private readonly ConcurrentDictionary<string, CacheLockEntry> _locks = [];

        public async ValueTask<ICacheLock> LockAsync(
            string key,
            CancellationToken cancellationToken = default)
        {
            var entry = _locks.GetOrAdd(
                key,
                _ => new CacheLockEntry());

            Interlocked.Increment(ref entry.ReferenceCount);

            await entry.Semaphore.WaitAsync(cancellationToken);

            return new CacheLockHandle(
                this,
                key,
                entry);
        }

        internal void Release(
            string key,
            CacheLockEntry entry)
        {
            if (Interlocked.Decrement(ref entry.ReferenceCount) == 0)
            {
                if (_locks.TryRemove(key, out _))
                {
                    entry.Semaphore.Dispose();
                }
            }
        }
    }
}
