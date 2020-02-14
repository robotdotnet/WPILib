using System;

namespace Hal
{
    public class UncleanStatusException : Exception
    {
        public int StatusCode { get; set; }
        public UncleanStatusException(int statusCode) : base($"Code: {statusCode} Message: {HALLowLevel.GetErrorMessage(statusCode)}")
        {
            StatusCode = statusCode;
        }

        public UncleanStatusException(int statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public UncleanStatusException()
        {
        }

        public UncleanStatusException(string message) : base(message)
        {
        }

        public UncleanStatusException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
