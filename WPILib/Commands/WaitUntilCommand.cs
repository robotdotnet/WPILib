using static WPILib.Timer;

namespace WPILib.Commands
{
    /// <summary>
    /// This command waits until an absolute game time.
    /// </summary>
    /// <remarks>This will wait until the game clock reaches
    /// some value, then will continue to the next command</remarks>
    public class WaitUntilCommand : Command
    {
        private readonly double m_time;

        /// <summary>
        /// Initializes a new <see cref="WaitUntilCommand"/>.
        /// </summary>
        /// <param name="time">The time to wait until.</param>
        public WaitUntilCommand(double time) : base($"WaitUntil({time})")
        {
            m_time = time;
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
        protected override bool IsFinished() => GetMatchTime() >= m_time;

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
