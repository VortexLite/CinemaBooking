using Microsoft.Extensions.DependencyInjection;

namespace Movie.Application.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            
            return services;
        }
    }
}
