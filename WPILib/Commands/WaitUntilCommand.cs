using static WPILib.Timer;

namespace WPILib.Commands
{
    public class WaitUntilCommand : Command
    {
        private double m_time;

        public WaitUntilCommand(double time) : base($"WaitUntil({time})")
        {
            m_time = time;
        }


        protected override void Initialize()
        {
        }

        protected override void Execute()
        {
        }

        protected override bool IsFinished() => MatchTime >= m_time;

        protected override void End()
        {
        }

        protected override void Interrupted()
        {
        }
    }
}
