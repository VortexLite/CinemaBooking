using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movie.Infrastructure.Persistence;
using Movie.Infrastructure.Persistence.Interceptors;

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

            services.AddCaching(configuration);

            services.AddMessaging(configuration);

            services.AddRepositories();

            return services;
        }
    }
}
