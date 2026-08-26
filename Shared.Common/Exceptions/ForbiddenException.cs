using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Common.Exceptions
{
    public sealed class ForbiddenException : ApiException
    {
        public ForbiddenException(string message)
            : base(
                "Forbidden",
                message,
                StatusCodes.Status403Forbidden) { }
    }
}
