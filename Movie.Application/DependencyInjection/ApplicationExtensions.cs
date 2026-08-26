using Microsoft.Extensions.DependencyInjection;

namespace Movie.Application.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddMediatR();

            services.AddValidation();

            services.AddMapster();

            return services;
        }
    }
}
