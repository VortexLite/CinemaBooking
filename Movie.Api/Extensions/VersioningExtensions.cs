using Asp.Versioning;

namespace Movie.Api.Extensions
{
    public static class VersioningExtensions
    {
        public static IServiceCollection AddApiVersioning(
            this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);

                options.AssumeDefaultVersionWhenUnspecified = true;

                options.ReportApiVersions = true;
            });

            return services;
        }
    }
}
