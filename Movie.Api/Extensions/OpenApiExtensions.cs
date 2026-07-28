using Microsoft.OpenApi;
using Movie.Api.OpenAPI;
using System.Reflection.Metadata;
using System.Xml.Linq;

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
