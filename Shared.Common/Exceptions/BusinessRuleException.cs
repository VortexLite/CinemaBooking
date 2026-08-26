using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Common.Exceptions
{
    public sealed class BusinessRuleException : ApiException
    {
        public BusinessRuleException(string message)
            : base(
                "Business rule violation",
                message,
                StatusCodes.Status400BadRequest)
        {
        }
    }
}
