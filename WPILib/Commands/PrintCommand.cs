using System;

namespace WPILib.Commands
{
    /// <summary>
    /// A <see cref="PrintCommand"/> is a command which prints out a string when it is initialized, and then immediately finishes.
    /// </summary>
    /// <remarks>It is useful if you want a <see cref="CommandGroup"/> to print out a string when it reaches a certain point.</remarks>
    public class PrintCommand : InstantCommand
    {
        private readonly string m_message;

        /// <summary>
        /// Instantiates a <see cref="PrintCommand"/> which will print the given message when it is run.
        /// </summary>
        /// <param name="message">The message to print.</param>
        public PrintCommand(string message) : base("Print(\"" + message + "\"")
        {
            m_message = message;
        }

        ///<inheritdoc/>
        protected override void Initialize()
        {
            Console.WriteLine(m_message);
        }
    }
}
