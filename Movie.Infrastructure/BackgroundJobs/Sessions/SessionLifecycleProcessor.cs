using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Movie.Application.Abstractions.Interfaces;
using Movie.Application.Common.Interfaces;
using Movie.Domain.Common;

namespace Movie.Infrastructure.BackgroundJobs.Sessions
{
    public sealed class SessionLifecycleProcessor
        : ISessionLifecycleProcessor
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TimeProvider _timeProvider;
        private readonly SessionLifecycleOptions _options;
        private readonly ILogger<SessionLifecycleProcessor> _logger;

        public SessionLifecycleProcessor(ISessionRepository sessionRepository,
            IUnitOfWork unitOfWork,
            TimeProvider timeProvider,
            IOptions<SessionLifecycleOptions> options,
            ILogger<SessionLifecycleProcessor> logger)
        {
            _sessionRepository = sessionRepository;
            _unitOfWork = unitOfWork;
            _timeProvider = timeProvider;
            _options = options.Value;
            _logger = logger;
        }

        public async Task ProcessAsync(CancellationToken cancellationToken = default)
        {
            var utcNow = _timeProvider.GetUtcNow().UtcDateTime;

            await StartSessionsAsync(utcNow, cancellationToken);

            await FinishSessionsAsync(utcNow, cancellationToken);
        }

        private async Task StartSessionsAsync(DateTime utcNow, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var sessions = await _sessionRepository.GetSessionToStartAsync(
                    utcNow, _options.BatchSize, cancellationToken);

                if (sessions.Count == 0)
                    break;

                foreach (var session in sessions)
                {
                    session.Start();

                    _logger.LogInformation("Session {SessionId} automatically started", session.Id);
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (sessions.Count < _options.BatchSize)
                    break;
            }
        }

        private async Task FinishSessionsAsync(DateTime utcNow, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var sessions = await _sessionRepository.GetSessionToFinishAsync(
                    utcNow, _options.BatchSize, cancellationToken);

                if (sessions.Count == 0)
                    return;

                foreach (var session in sessions)
                {
                    session.Finish();

                    _logger.LogInformation("Session {SessionId} automatically finished", session.Id);
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (sessions.Count < _options.BatchSize)
                    break;
            }
        }
    }
}
