using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Movie.Infrastructure.DependencyInjection
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddServices();

            services.AddDatabase(configuration);

            services.AddRedisCache(configuration);

            services.AddMessaging(configuration);

            services.AddRepositories();

            services.AddQueries();

            services.AddRabbitMq(configuration);

            services.AddSessionLifecycle(configuration);

            return services;
        }
    }
}
