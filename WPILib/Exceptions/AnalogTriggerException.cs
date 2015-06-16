using System;

namespace WPILib.Exceptions
{
    /// <summary>
    /// Exceptions dealing with improper operation of the Analog trigger.
    /// </summary>
    public class AnalogTriggerException : SystemException
    {
        /// <summary>
        /// Create a new exception with the given message
        /// </summary>
        /// <param name="message">The message to pass to the exception</param>
        public AnalogTriggerException(string message)
            : base(message)
        {
        }
    }
}
