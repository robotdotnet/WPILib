using System;

namespace Hal
{
    public class AllocationException : UncleanStatusException
    {
        public int Requested { get; }
        public int Min { get; }
        public int Max { get; }

        public AllocationException(int statusCode, int requested, int min, int max)
            : base(statusCode, $"Code: {statusCode} Message: {HALLowLevel.GetErrorMessage(statusCode)}: min {min} max {max} requested {requested}")
        {
            Requested = requested;
            Min = min;
            Max = max;
        }

        public AllocationException()
        {
        }

        public AllocationException(string message) : base(message)
        {
        }

        public AllocationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
