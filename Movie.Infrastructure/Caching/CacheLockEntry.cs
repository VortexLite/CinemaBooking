namespace Movie.Infrastructure.Caching
{
    internal sealed class CacheLockEntry
    {
        public SemaphoreSlim Semaphore { get; } = new(1, 1);

        public int ReferenceCount;
    }
}
