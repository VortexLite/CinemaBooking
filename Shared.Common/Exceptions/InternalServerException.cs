using Microsoft.AspNetCore.Http;

namespace Shared.Common.Exceptions
{
    public sealed class InternalServerException : ApiException
    {
        public InternalServerException(string message)
            : base("Internal Server Error", message, StatusCodes.Status500InternalServerError) { }
    }
}
