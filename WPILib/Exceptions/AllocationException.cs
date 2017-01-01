using System;

namespace WPILib.Exceptions
{
    /// <summary>
    /// Exception indicating that the resource is already allocated.
    /// </summary>
    public class AllocationException : Exception
    {
        /// <summary>
        /// Gets the status code of the exception
        /// </summary>
        public int Status { get; private set; }

        /// <summary>
        /// Create a new AllocationException
        /// </summary>
        /// <param name="message">The message to attach to the exception</param>
        public AllocationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Create a new AllocationException
        /// </summary>
        /// <param name="message">The message to attach to the exception</param>
        /// <param name="status">The status creating this allocation exception</param>
        public AllocationException(string message, int status)
            : base(message)
        {
            Status = status;
        }
    }
}