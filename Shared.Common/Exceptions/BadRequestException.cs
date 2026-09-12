using Microsoft.AspNetCore.Http;

namespace Shared.Common.Exceptions
{
    public sealed class BadRequestException : ApiException
    {
        public BadRequestException(string message)
            : base("Bad Request", message, StatusCodes.Status400BadRequest) { }
    }
}
