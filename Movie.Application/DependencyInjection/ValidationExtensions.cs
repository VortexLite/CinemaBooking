using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

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
