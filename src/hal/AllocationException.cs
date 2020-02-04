using System;
using System.Collections.Generic;
using System.Text;

namespace Hal
{
    public class AllocationException : UncleanStatusException
    {
        public int Requested { get; }
        public int Min { get; }
        public int Max { get; }

        public AllocationException(int statusCode, int requested, int min, int max)
            : base(statusCode, $"Code: {statusCode} Message: {HalBase.GetErrorMessage(statusCode)}: min {min} max {max} requested {requested}")
        {
            Requested = requested;
            Min = min;
            Max = max;
        }
    }
}
