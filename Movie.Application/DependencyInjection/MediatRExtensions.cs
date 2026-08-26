using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Movie.Application.Common.Behaviors;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Movie.Application.DependencyInjection
{
    public static class MediatRExtensions
    {
        public static IServiceCollection AddMediatR(
            this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(
                    Assembly.GetExecutingAssembly());

                cfg.AddOpenBehavior(typeof(RequestLoggingBehavior<,>));

                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));

                cfg.AddOpenBehavior(typeof(PerformanceBehavior<,>));

                cfg.AddOpenBehavior(typeof(CachingBehavior<,>));
            });

            return services;
        }
    }
}
