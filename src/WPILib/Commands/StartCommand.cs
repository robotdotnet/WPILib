namespace WPILib.Commands
{
    /// <summary>
    /// A <see cref="StartCommand"/> will call the <see cref="Command.Start">Start()</see> method of another
    /// command when it is initialized and will finish immediately.
    /// </summary>
    public class StartCommand : InstantCommand
    {
        private readonly Command m_commandToFork;

        /// <summary>
        /// Instantiates a <see cref="StartCommand"/> which will start the given command when its
        /// <see cref="Initialize"/> is called.
        /// </summary>
        /// <param name="commandToStart">The <see cref="Command"/> to start.</param>
        public StartCommand(Command commandToStart) : base("Start(" + commandToStart + ")")
        {
            m_commandToFork = commandToStart;
        }

        ///<inheritdoc/>
        protected override void Initialize() => m_commandToFork.Start();
    }
}
