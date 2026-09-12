namespace Movie.Application.Abstractions.Interfaces
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key,
            CancellationToken cancellation = default);

        Task SetAsync<T>(string key, 
            T value, 
            TimeSpan? expiration = null,
            IEnumerable<string>? tags = null,
            CancellationToken cancellationToken = default);

        Task RemoveAsync(string key,
            CancellationToken cancellationToken = default);

        Task RemoveByPrefixAsync(string prefix,
            CancellationToken cancellationToken = default);

        Task RemoveByTagAsync(string tag,
            CancellationToken cancellationToken = default);
    }
}
