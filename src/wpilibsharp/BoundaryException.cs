using System;

namespace WPILib
{
    public class BoundaryException : Exception
    {
        public BoundaryException(string message)
            : base(message)
        {

        }

        public BoundaryException()
        {
        }

        public BoundaryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
