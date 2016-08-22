using System;

namespace WPILib.Exceptions
{
    /// <summary>
    /// Exception indicating that the resource is already allocated.
    /// </summary>
    public class AllocationException : Exception
    {
        public int Status { get; private set; }

        /// <summary>
        /// Create a new AllocationException
        /// </summary>
        /// <param name="message">The message to attach to the exception</param>
        public AllocationException(int status, string message)
            : base(message)
        {
            Status = status;
        }
    }
}