using System;

namespace WPILib.Util
{
    /// <summary>
    /// Exception indicating that the resource is already allocated.
    /// This is meant to be thrown by the resource class.
    /// </summary>
    public class CheckedAllocationException : Exception
    {
        /// <summary>
        /// Create a new CheckedAllocationException
        /// </summary>
        /// <param name="message">The message to attach to the exception</param>
        public CheckedAllocationException(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// Exception indicating that the resource is already allocated.
    /// </summary>
    public class AllocationException : SystemException
    {
        /// <summary>
        /// Create a new AllocationException
        /// </summary>
        /// <param name="message">The message to attach to the exception</param>
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

    /// <summary>
    /// This exception represent an error in which a lower limit was set as higher
    /// then an upper limit.
    /// </summary>
    public class BoundaryException : SystemException
    {
        /// <summary>
        /// Create a new exception with the given message
        /// </summary>
        /// <param name="message">The message to attach to the exception</param>
        public BoundaryException(string message)
            : base(message)
        {
        }
        
        /// <summary>
        /// Make sure that the given value is between the upper and lower bounds, and
	    /// throw an exception if they are not.
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="lower">The minimum acceptable value.</param>
        /// <param name="upper">The maximum acceptable value.</param>
        public static void AssertWithinBounds(double value, double lower, double upper)
        {
            if (value < lower || value > upper)
                throw new BoundaryException("Value must be between " + lower
                    + " and " + upper + ", " + value + " given");
        }

        /// <summary>
        /// Returns the message for a boundary exception. Used to keep  the message 
        /// consistant across all boundary exceptions.
        /// </summary>
        /// <param name="value">The given value</param>
        /// <param name="lower">The lower limit</param>
        /// <param name="upper">The upper limit</param>
        /// <returns>The message for a boundary exception</returns>
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

    /// <summary>
    /// Thrown if there is an error caused by a basic system or setting
    /// not being properly initialized before being used.
    /// </summary>
    public class BaseSystemNotInitializedException : SystemException
    {
        /// <summary>
        /// Create a new BaseSystemNotInitializedException
        /// </summary>
        /// <param name="message">The message to attach to the exception</param>
        public BaseSystemNotInitializedException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Create a new BaseSystemNotInitializedException using the offending class that was not set and the
	    /// class that was affected.
        /// </summary>
        /// <param name="offender">The class or interface that was not properly initialized.</param>
        /// <param name="affected">The class that was was affected by this missing initialization.</param>
        public BaseSystemNotInitializedException(object offender, object affected)
            : base("The " + offender.GetType().Name + " for the " + affected.GetType().Name + " was never set.")
        {

        }

    }

    /// <summary>
    /// Exception for bad status codes from the chip object
    /// </summary>
    public class UncleanStatusException : InvalidOperationException
    {

        private int m_statusCode;

        /// <summary>
        /// Create a new UncleanStatusException
        /// </summary>
        /// <param name="status">The status code the caused the exception</param>
        /// <param name="message">A message describing the exception</param>
        public UncleanStatusException(int status, String message)
            : base(message)
        {
            m_statusCode = status;
        }

        /// <summary>
         /// Create a new UncleanStatusException
         /// </summary>
         /// <param name="status">The status code that caused the exception</param>
        public UncleanStatusException(int status) :
            this(status, "Status code was non-zero")
        {

        }

        /// <summary>
        /// Create a new UncleanStatusException
        /// </summary>
        /// <param name="message">A message describing the exception</param>
        public UncleanStatusException(String message) : this(-1, message)
        {
        }

        /// <summary>
        /// Create a new UncleanStatusException
        /// </summary>
        public UncleanStatusException() :this(-1, "Status code was non-zero")
        {
            
        }

        /// <summary>
        /// Create a new UncleanStatusException
        /// </summary>
        /// <returns>The status code that caused the exception</returns>
        public int GetStatus()
        {
            return m_statusCode;
        }
    }

}
