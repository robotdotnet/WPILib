using System;

namespace WPILib.Exceptions
{
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
}