using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.DependencyInjection
{
    public static class AspireInfrastructureExtensions
    {
        public static IHostApplicationBuilder AddInfrastructureAspire(
            this IHostApplicationBuilder builder)
        {
            builder.AddRabbitMQClient("rabbitmq");

            builder.AddRedisClient("redis");

            return builder;
        }
    }
}
