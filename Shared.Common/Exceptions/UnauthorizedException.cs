using Microsoft.AspNetCore.Http;

namespace Shared.Common.Exceptions
{
    public sealed class UnauthorizedException : ApiException
    {
        public UnauthorizedException(string message)
            : base("Unauthorized", message, StatusCodes.Status401Unauthorized) { }
    }
}
