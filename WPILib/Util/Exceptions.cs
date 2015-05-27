

using System;

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

    public class InvalidValueException : SystemException
    {
        public InvalidValueException(string message)
            : base(message)
        {

        }
    }

    public class BaseSystemNotInitializedException : SystemException
    {
        public BaseSystemNotInitializedException(string message)
            : base(message)
        {

        }

        public BaseSystemNotInitializedException(string offender, string affected)
            : base("The " + offender + " for the " + affected + " was never set.")
        {

        }

    }

    public class UncleanStatusException : InvalidOperationException
    {

        private int statusCode;

        /**
         * Create a new UncleanStatusException
         * @param status the status code that caused the exception
         * @param message A message describing the exception
         */
        public UncleanStatusException(int status, String message)
            : base(message)
        {
            statusCode = status;
        }

        /**
         * Create a new UncleanStatusException
         * @param status the status code that caused the exception
         */
        public UncleanStatusException(int status) :
            this(status, "Status code was non-zero")
        {

        }

        /**
         * Create a new UncleanStatusException
         * @param message a message describing the exception
         */
        public UncleanStatusException(String message) : this(-1, message)
        {
        }

        /**
         * Create a new UncleanStatusException
         */
        public UncleanStatusException() :this(-1, "Status code was non-zero")
        {
            
        }

        /**
         * Create a new UncleanStatusException
         * @return the status code that caused the exception
         */
        public int GetStatus()
        {
            return statusCode;
        }
    }

}
