using Movie.Application.Common.Interfaces;
using System.Collections.Concurrent;

namespace Movie.Infrastructure.Caching
{
    public sealed class MemoryCacheTagStore
        : ICacheTagStore
    {
        private readonly ConcurrentDictionary<string,
            ConcurrentDictionary<string, byte>> _tags = [];

        public Task AddAsync(string key, 
            IEnumerable<string> tags, 
            CancellationToken cancellationToken = default)
        {
            foreach (var tag in tags)
            {
                var keys = _tags.GetOrAdd(tag, _ => new ConcurrentDictionary<string, byte>());

                keys.TryAdd(key, 0);
            }

            return Task.CompletedTask;
        }

        public Task<IReadOnlyCollection<string>> GetKeysAsync(string tag, 
            CancellationToken cancellationToken = default)
        {
            if (_tags.TryGetValue(tag, out var keys))
                return Task.FromResult<IReadOnlyCollection<string>>(keys.Keys.ToArray());

            return Task.FromResult<IReadOnlyCollection<string>>(Array.Empty<string>());
        }

        public Task RemoveTagAsync(string tag, 
            CancellationToken cancellationToken = default)
        {
            _tags.TryRemove(tag, out _);

            return Task.CompletedTask;
        }

        public Task RemoveKeyAsync(string key,
            CancellationToken cancellationToken = default)
        {
            foreach (var tag in _tags.Values)
                tag.TryRemove(key, out _);

            return Task.CompletedTask;
        }
    }
}
