using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movie.Application.Common.Interfaces;
using Movie.Infrastructure.BackgroundJobs.Sessions;

namespace Movie.Infrastructure.DependencyInjection
{
    public static class SessionLifecycleExtensions
    {
        public static IServiceCollection AddSessionLifecycle(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<SessionLifecycleOptions>(
                configuration.GetSection(SessionLifecycleOptions.SectionName));

            services.AddScoped<ISessionLifecycleProcessor, SessionLifecycleProcessor>();

            services.AddHostedService<SessionLifecycleBackgroundService>();

            return services;
        }
    }
}
