using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Movie.Domain.Exceptions;
using Shared.Common.Exceptions;

namespace Movie.Api.Exceptions
{
    public sealed class GlobalExceptionHandler
        : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception,
                "Exception occurred: {Message}",
                exception.Message);

            var problem = new ProblemDetails
            {
                Instance = httpContext.Request.Path
            };

            if (exception is ValidationException validationException)
            {
                problem.Title = "Validation failed";
                problem.Status = StatusCodes.Status400BadRequest;
                problem.Detail = "One or more validation errors occurred.";

                problem.Extensions["errors"] = validationException.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(x => x.ErrorMessage));
            }
            else if (exception is ApiException appException)
            {
                problem.Title = appException.Title;
                problem.Status = appException.StatusCode;
                problem.Detail = appException.Message;
            }
            else if (exception is DomainException domainException)
            {
                problem.Title = "Domain Error";
                problem.Status = StatusCodes.Status400BadRequest;
                problem.Detail = domainException.Message;
            }
            else
            {
                problem.Title = "Internal Server Error";
                problem.Status = StatusCodes.Status500InternalServerError;
                problem.Detail = "An unexpected error occurred.";
            }


            httpContext.Response.StatusCode = problem.Status!.Value;

            await httpContext.Response.WriteAsJsonAsync(
                problem,
                cancellationToken);

            return true;
        }
    }

}
