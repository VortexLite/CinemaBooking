using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Common.Exceptions
{
    public sealed class BadRequestException : ApiException
    {
        public BadRequestException(string message)
            : base(
                "Bad Request",
                message,
                StatusCodes.Status400BadRequest)
        {
        }
    }
}
