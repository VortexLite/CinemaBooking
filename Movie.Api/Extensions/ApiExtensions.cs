using Mapster;
using Movie.Api.Exceptions;

namespace Movie.Api.Extensions
{
    public static class ApiExtensions
    {
        public static IServiceCollection AddExtensions(
            this IServiceCollection services)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();

            services.AddProblemDetails();

            return services;
        }
    }
}
