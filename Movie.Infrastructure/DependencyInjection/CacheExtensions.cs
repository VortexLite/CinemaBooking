using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.DependencyInjection
{
    public static class CacheExtensions
    {
        public static IServiceCollection AddCaching(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            return services;
        }
    }
}
