using System;
using WPILib;
using WPILib.SmartDashboard;

namespace WPILib2.Commands
{
    public class WaitCommand : CommandBase
    {
        protected Timer m_timer = new Timer();
        private readonly TimeSpan m_duration;

        public WaitCommand(TimeSpan time)
        {
            m_duration = time;
            SendableRegistry.Instance.SetName(this, $"{Name}: {time.TotalSeconds} seconds");
        }

        public override void Initialize()
        {
            m_timer.Reset();
            m_timer.Start();
        }

        public override bool IsFinished => m_timer.HasPeriodPassed(m_duration);

        public override bool RunsWhenDisabled => true;
    }
}
