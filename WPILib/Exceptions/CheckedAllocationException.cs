using System;

namespace WPILib.Exceptions
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
}