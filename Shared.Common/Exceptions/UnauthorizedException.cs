using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Common.Exceptions
{
    public sealed class UnauthorizedException : ApiException
    {
        public UnauthorizedException(string message)
            : base(
                "Unauthorized",
                message,
                StatusCodes.Status401Unauthorized) { }
    }
}
