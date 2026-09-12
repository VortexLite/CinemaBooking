namespace Movie.Application.Common.Interfaces
{
    public interface ISessionLifecycleProcessor
    {
        Task ProcessAsync(CancellationToken cancellationToken = default);
    }
}
