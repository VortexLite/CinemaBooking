namespace Movie.Api.Extensions
{
    public static class HealthChecksExtensions
    {
        public static IServiceCollection AddHealthChecks(
            this IServiceCollection services)
        {
            services.AddHealthChecks();

            return services;
        }
    }
}
