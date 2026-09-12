using Microsoft.AspNetCore.Http;

namespace Shared.Common.Exceptions
{
    public sealed class ConflictException : ApiException
    {
        public ConflictException(string message)
            : base("Conflict", message, StatusCodes.Status409Conflict) { }
    }
}
