using System;

namespace WPILib.Exceptions
{
    /// <summary>
    /// This exception represent an error in which a lower limit was set as higher
    /// then an upper limit.
    /// </summary>
    public class BoundaryException : Exception
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
        /// consistent across all boundary exceptions.
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
}