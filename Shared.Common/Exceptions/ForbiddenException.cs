using Microsoft.AspNetCore.Http;

namespace Shared.Common.Exceptions
{
    public sealed class ForbiddenException : ApiException
    {
        public ForbiddenException(string message)
            : base("Forbidden", message, StatusCodes.Status403Forbidden) { }
    }
}
