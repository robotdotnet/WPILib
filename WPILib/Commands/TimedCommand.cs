namespace WPILib.Commands
{
    /// <summary>
    /// This command will wait for a timeout before finishing
    /// </summary>
    public class TimedCommand : Command
    {
        public TimedCommand(double timeout) : base(timeout) { }

        /// <summary>
        /// Creates a new <see cref="TimedCommand"/> with the given name
        /// </summary>
        public TimedCommand(string name, double timeout) : base(name, timeout) { }

        ///<inheritdoc/>
        protected override bool IsFinished() => IsTimedOut();
    }
}
