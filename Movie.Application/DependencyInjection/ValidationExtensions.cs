using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Movie.Application.DependencyInjection
{
    public static class ValidationExtensions
    {
        public static IServiceCollection AddValidation(
            this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(
                Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
