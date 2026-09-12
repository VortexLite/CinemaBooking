using Movie.Api.OpenAPI;

namespace Movie.Api.Extensions
{
    public static class OpenApiExtensions
    {
        public static IServiceCollection AddOpenApiDocumentation(
            this IServiceCollection services)
        {
            services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
            });

            return services;
        }
    }
}
