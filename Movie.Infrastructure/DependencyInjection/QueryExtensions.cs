using Microsoft.Extensions.DependencyInjection;
using Movie.Application.Common.Interfaces;
using Movie.Infrastructure.Persistence.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.DependencyInjection
{
    public static class QueryExtensions
    {
        public static IServiceCollection AddQueries(
            this IServiceCollection services)
        {
            services.AddScoped<IMovieQueries, MovieQueries>();

            services.AddScoped<IGenreQueries, GenreQueries>();

            return services;
        }
    }
}
