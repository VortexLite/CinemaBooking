using Microsoft.AspNetCore.Http;

namespace Shared.Common.Exceptions
{
    public sealed class BusinessRuleException : ApiException
    {
        public BusinessRuleException(string message)
            : base("Business rule violation", message, StatusCodes.Status400BadRequest) { }
    }
}
