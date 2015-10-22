using System;

namespace WPILib.Exceptions
{
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
        /// <param name="affected">The class that was affected by this missing initialization.</param>
        public BaseSystemNotInitializedException(object offender, object affected)
            : base("The " + (offender?.GetType()?.Name ?? "Implementation") + " for the " + affected.GetType().Name + " was never set.")
        {

        }

    }
}