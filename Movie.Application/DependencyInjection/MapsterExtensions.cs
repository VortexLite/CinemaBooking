using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Movie.Application.DependencyInjection
{
    public static class MapsterExtensions
    {
        public static IServiceCollection AddMapster(
            this IServiceCollection services)
        {

            var config = TypeAdapterConfig.GlobalSettings;

            config.Scan(typeof(ApplicationExtensions).Assembly);

            services.AddSingleton(config);

            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}
