using System;

namespace WPILib.Exceptions
{
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