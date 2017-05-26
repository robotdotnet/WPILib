using NetworkTables.Tables;
using WPILib.Interfaces;

namespace WPILib.Commands
{
    /// <summary>
    /// This class defines a <see cref="Command"/> which interacts heavily with a PID loop.
    /// </summary>
    /// <remarks>It provides some conveniance methods to run an internal <see cref="PIDController"/>.
    /// It will also start and stop said <see cref="PIDController"/> when the <see cref="ConditionalCommand"/>
    /// is first initialized and ended/interrupted.</remarks>
    ///
    /// <summary>
    /// A <see cref="ConditionalCommand"/> is a <see cref="Command"/> that starts one of two commands.
    /// </summary>
    ///
    /// <remarks>
    /// A <see cref="ConditionalCommand"/> uses m_condition to determine whether it should run m_onTrue or
    /// m_onFalse.
    ///
    /// A <see cref="ConditionalCommand"/> adds the proper <see cref="Command"/> to the <see cref="Scheduler"/> during
    /// <see cref="ConditionalCommand.Initialize()"/> and then <see cref="ConditionalCommand.IsFinished()"/> will
    /// return true once that <see cref="Command"/> has finished executing.
    ///
    /// If no <see cref="Command"/> is specified for m_onFalse, the occurrence of that condition will be a
    /// no-op.
    /// </remarks>
    ///
    /// @see Command
    /// @see Scheduler
    ///
    public abstract class ConditionalCommand : Command
    {
        private readonly Command m_onTrue;
        private readonly Command m_onFalse;
        private Command m_chosenCommand;

        /// <summary>
        /// Creates a new <see cref="ConditionalCommand"/> with given onTrue and onFalse Commands.
        /// </summary>
        /// <param name="onTrue">Command to run when condition is true</param>
        public ConditionalCommand(Command onTrue)
            : this(onTrue, new InstantCommand())
        {
        }

        /// <summary>
        /// Creates a new <see cref="ConditionalCommand"/> with given onTrue and onFalse Commands.
        /// </summary>
        /// <param name="onTrue">Command to run when condition is true</param>
        /// <param name="onFalse">Command to run when condition is false</param>
        public ConditionalCommand(Command onTrue, Command onFalse)
        {
            m_onTrue = onTrue;
            m_onFalse = onFalse;

            foreach (var sys in onTrue.GetRequirements())
            {
                Requires(sys);
            }
            foreach (var sys in onFalse.GetRequirements())
            {
                Requires(sys);
            }
        }

        /// <summary>
        /// Creates a new <see cref="ConditionalCommand"/> with given onTrue and onFalse Commands.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="onTrue">Command to run when condition is true</param>
        public ConditionalCommand(string name, Command onTrue)
            : this(name, onTrue, new InstantCommand())
        {
        }

        /// <summary>
        /// Creates a new <see cref="ConditionalCommand"/> with given onTrue and onFalse Commands.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="onTrue">Command to run when condition is true</param>
        /// <param name="onFalse">Command to run when condition is false</param>
        public ConditionalCommand(string name, Command onTrue, Command onFalse)
            : base(name)
        {
            m_onTrue = onTrue;
            m_onFalse = onFalse;

            foreach (var sys in onTrue.GetRequirements())
            {
                Requires(sys);
            }
            foreach (var sys in onFalse.GetRequirements())
            {
                Requires(sys);
            }
        }

        /// <summary>
        /// The Condition to test to determine which Command to run.
        /// </summary>
        ///
        /// @return true if m_onTrue should be run or false if m_onFalse should be run.
        ///
        protected abstract bool Condition();

        /// <summary>
        /// Calls <see cref="ConditionalCommand.Condition"/> and runs the proper command.
        /// </summary>
        protected internal override void _Initialize()
        {
            m_chosenCommand = Condition() ? m_onTrue : m_onFalse;

            // This is a hack to make cancelling the chosen command inside a CommandGroup work properly
            m_chosenCommand.ClearRequirements();

            m_chosenCommand.Start();
        }

        protected new void _Cancel()
        {
            if (m_chosenCommand != null && m_chosenCommand.IsRunning())
            {
                m_chosenCommand.Cancel();
            }

            base._Cancel();
        }

        protected override bool IsFinished()
        {
            return m_chosenCommand != null && m_chosenCommand.IsRunning()
                && m_chosenCommand._IsFinished();
        }

        protected override void Interrupted()
        {
            if (m_chosenCommand != null && m_chosenCommand.IsRunning())
            {
                m_chosenCommand.Cancel();
            }

            base.Interrupted();
        }

    }
}
