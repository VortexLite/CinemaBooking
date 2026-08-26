using Movie.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Caching
{
    public sealed class CacheLockHandle : ICacheLock
    {
        private readonly CacheLockProvider _provider;
        private readonly string _key;
        private readonly CacheLockEntry _entry;

        internal CacheLockHandle(
            CacheLockProvider provider,
            string key,
            CacheLockEntry entry)
        {
            _provider = provider;
            _key = key;
            _entry = entry;
        }

        public ValueTask DisposeAsync()
        {
            _entry.Semaphore.Release();

            _provider.Release(_key, _entry);

            return ValueTask.CompletedTask;
        }
    }
}
