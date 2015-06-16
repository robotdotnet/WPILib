using System;

namespace WPILib.Exceptions
{
    /// <summary>
    /// This class represents errors in trying to set relay values contradictory
    /// to the direction to which the relay is set.
    /// </summary>
    public class InvalidValueException : SystemException
    {
        /// <summary>
        /// Create a new exception with the given message
        /// </summary>
        /// <param name="message">The message to pass with the exception</param>
        public InvalidValueException(string message)
            : base(message)
        {

        }
    }
}