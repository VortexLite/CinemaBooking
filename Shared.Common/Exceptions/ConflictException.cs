using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Common.Exceptions
{
    public sealed class ConflictException : ApiException
    {
        public ConflictException(string message)
            : base(
                "Conflict",
                message,
                StatusCodes.Status409Conflict) { }
    }
}
