namespace WPILib.Commands
{
    /// <summary>
    /// This command will execute once, then finish immediately afterward.
    ///
    /// Subclassing <see cref="InstantCommand"/> is shorthand for returning true from <see cref="Command.IsFinished"/>.
    /// </summary>
    public class InstantCommand : Command
    {
        public InstantCommand() { }

        /// <summary>
        /// Creates a new <see cref="InstantCommand"/> with the given name
        /// </summary>
        public InstantCommand(string name) : base(name) { }

        ///<inheritdoc/>
        protected override bool IsFinished() => true;
    }
}
