using System;

namespace WPILib.Commands
{
    /// <summary>
    /// This exception will be thrown if a command is used illegally.
    /// </summary>
    /// <remarks>There are several ways for this to happen.
    /// <para/>Basically, a command becomes "locked" after it is first started
    /// or added to a command group
    /// <para/>This exception should be thrown if (after a command has been locked)
    /// its requirements changes, it is put into multiple command groups,
    /// its is started from outside its command group, or it adds a new child.
    /// </remarks>
    public class IllegalUseOfCommandException : SystemException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IllegalUseOfCommandException"/> class
        /// </summary>
        public IllegalUseOfCommandException()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IllegalUseOfCommandException"/> class with the given message.
        /// </summary>
        /// <param name="message">The message</param>
        public IllegalUseOfCommandException(string message) : base(message)
        {
            
        }
    }
}
