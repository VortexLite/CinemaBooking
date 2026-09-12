using Microsoft.AspNetCore.Http;

namespace Shared.Common.Exceptions
{
    public sealed class NotFoundException : ApiException
    {
        public NotFoundException(string entity, object key)
            : base("Resource not found",
                $"{entity} with id '{key}' was not found.",
                StatusCodes.Status404NotFound) { }

        public NotFoundException(string message)
            : base(
                "Resource not found",
                message,
                StatusCodes.Status404NotFound) { }
    }
}
