using System;

namespace WPIUtil
{
    public class MethodGenerationException : Exception
    {
        public MethodGenerationException(string message)
            : base(message)
        {

        }

        public MethodGenerationException()
        {
        }

        public MethodGenerationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
