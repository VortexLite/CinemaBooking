using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Common.Exceptions
{
    public abstract class ApiException : Exception
    {
        protected ApiException(
        string title,
        string message,
        int statusCode)
        : base(message)
        {
            Title = title;
            StatusCode = statusCode;
        }

        public string Title { get; }
        public int StatusCode { get; }
    }
}
