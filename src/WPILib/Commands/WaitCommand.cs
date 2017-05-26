namespace WPILib.Commands
{
    /// <summary>
    /// A <see cref="WaitCommand"/> will wait for a certain amount of time before finishing.
    /// </summary>
    /// <remarks>It is useful if you want a <see cref="CommandGroup"/> to pause
    /// for a moment.</remarks>
    /// <seealso cref="CommandGroup"/>
    public class WaitCommand : Command
    {
        /// <summary>
        /// Instantiates a <see cref="WaitCommand"/> with the given timeout.
        /// </summary>
        /// <param name="timeout">The time the command takes to run.</param>
        public WaitCommand(double timeout) : base($"Wait({timeout})", timeout)
        {
        }

        /// <summary>
        /// Instantiates a <see cref="WaitCommand"/> with the given timeout.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="timeout">The time the command takes to run.</param>
        public WaitCommand(string name, double timeout) : base(name, timeout)
        {
            
        }

        ///<inheritdoc/>
        protected override void Initialize()
        {
        }
        ///<inheritdoc/>
        protected override void Execute()
        {
        }
        ///<inheritdoc/>
        protected override bool IsFinished() => IsTimedOut();

        ///<inheritdoc/>
        protected override void End()
        {
        }
        ///<inheritdoc/>
        protected override void Interrupted()
        {
        }
    }
}
