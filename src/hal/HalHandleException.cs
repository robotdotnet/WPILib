using System;

namespace Hal
{
    public class HalHandleException : UncleanStatusException
    {
        public HalHandleException(int statusCode) : base(statusCode)
        {

        }

        public HalHandleException()
        {
        }

        public HalHandleException(string message) : base(message)
        {
        }

        public HalHandleException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
