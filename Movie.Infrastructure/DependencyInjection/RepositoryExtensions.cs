using Microsoft.Extensions.DependencyInjection;
using Movie.Application.Abstractions.Interfaces;
using Movie.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.DependencyInjection
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(
            this IServiceCollection services)
        {
            services.AddScoped<IMovieRepository, MovieRepository>();

            services.AddScoped<IGenreRepository, GenreRepository>();

            services.AddScoped<IHallRepository, HallRepository>();

            services.AddScoped<ILanguageRepository, LanguageRepository>();

            services.AddScoped<ISeatRepository, SeatRepository>();

            services.AddScoped<ISessionRepository, SessionRepository>();

            return services;
        }
    }
}
