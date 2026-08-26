using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Common.Interfaces
{
    public interface ICacheTagStore
    {
        Task AddAsync(string key, IEnumerable<string> tags,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<string>> GetKeysAsync(string tag,
            CancellationToken cancellationToken = default);

        Task RemoveTagAsync(string tag,
            CancellationToken cancellationToken = default);

        Task RemoveKeyAsync(string key,
            CancellationToken cancellationToken = default);
    }
}
