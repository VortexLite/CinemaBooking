namespace Movie.Application.Common.Interfaces
{
    public interface ICacheLockProvider
    {
        ValueTask<ICacheLock> LockAsync(string key,
            CancellationToken cancellationToken = default);
    }
}
