using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Messaging.RabbitMQ.Topology
{
    public sealed class RabbitMqTopologyHostedService
        : IHostedService
    {
        private readonly IRabbitMqTopologyInitializer _initializer;
        private readonly ILogger<RabbitMqTopologyHostedService> _logger;

        public RabbitMqTopologyHostedService(
            IRabbitMqTopologyInitializer initializer, 
            ILogger<RabbitMqTopologyHostedService> logger)
        {
            _initializer = initializer;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Initializing Movie RabbitMQ topology.");

            await _initializer.InitializeAsync(cancellationToken);

            _logger.LogInformation("Movie RabbitMQ topology initialized.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
