

using System;
using System.Collections.Generic;
using System.Text;

namespace WPILib.Util
{
    public class CheckedAllocationException : Exception
    {
        public CheckedAllocationException(string message)
            : base(message)
        {
        }
    }

    public class AllocationException : SystemException
    {
        public AllocationException(string message)
            : base(message)
        {
        }
    }

    public class AnalogTriggerException : SystemException
    {
        public AnalogTriggerException(string message)
            : base(message)
        {
        }
    }

    public class BoundaryException : SystemException
    {
        public BoundaryException(string message)
            : base(message)
        {
        }

        public static void AssertWithinBounds(double value, double lower, double upper)
        {
            if (value < lower || value > upper)
                throw new BoundaryException("Value must be between " + lower
                    + " and " + upper + ", " + value + " given");
        }

        public static string GetMessage(double value, double lower, double upper)
        {
            return "Value must be between " + lower + " and " + upper + ", "
                + value + " given";
        }
    }
}
