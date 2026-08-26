using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Common.Exceptions
{
    public sealed class InternalServerException : ApiException
    {
        public InternalServerException(string message)
            : base(
                "Internal Server Error",
                message,
                StatusCodes.Status500InternalServerError)
        {
        }
    }
}
