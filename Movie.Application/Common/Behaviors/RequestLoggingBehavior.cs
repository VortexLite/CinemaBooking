using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Movie.Application.Common.Behaviors
{
    public sealed class RequestLoggingBehavior<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<RequestLoggingBehavior<TRequest, TResponse>> _logger;

        public RequestLoggingBehavior(ILogger<RequestLoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            string requestName = typeof(TRequest).Name;

            string requestId = Activity.Current?.Id ?? Guid.CreateVersion7().ToString();

            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["RequestId"] = requestId,
                ["RequestName"] = requestName
            }))
            {
                bool isCommand = requestName.EndsWith("Command");

                if (isCommand)
                {
                    _logger.LogInformation("Handling command {CommandName}: {@Request}",
                        requestName, request);
                }
                else
                {
                    _logger.LogInformation("Handling query {QueryName}", requestName);
                }

                TResponse response = await next();

                _logger.LogInformation("Completed {RequestName}", requestName);

                return response;
            }
        }
    }
}
