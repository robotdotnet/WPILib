using System;
using System.Collections.Generic;
using System.Text;

namespace WPILib
{
    public class TimedRobot : IterativeRobotBase
    {
        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromMilliseconds(20);

        public override void StartCompetition()
        {
            RobotInit();

            Hal.DriverStation.ObserveUserProgramStarting();

            m_expirationTime = Timer.FPGATimestamp + m_period;
            UpdateAlarm();

            while (true)
            {
                ulong curTime = Hal.Notifier.WaitForAlarm(m_notifier, out var status);
                if (curTime == 0 || status != 0) break;

                m_expirationTime += m_period;

                UpdateAlarm();

                LoopFunc();
            }
        }

        public override void EndCompetition()
        {
            Hal.Notifier.Stop(m_notifier);
        }

        public TimeSpan Period => m_period;

        public TimedRobot() : this(DefaultPeriod)
        {

        }

        public TimedRobot(TimeSpan period) : base(period)
        {
            m_notifier = Hal.Notifier.Initialize();
            Hal.Notifier.SetName(m_notifier, "TimedRobot");

            // Report
        }

        public override void Dispose()
        {
            Hal.Notifier.Stop(m_notifier);
            Hal.Notifier.Clean(m_notifier);

            base.Dispose();
        }

        private int m_notifier;
        private TimeSpan m_expirationTime;

        private void UpdateAlarm()
        {
            Hal.Notifier.UpdateAlarm(m_notifier, (ulong)(m_expirationTime.TotalMilliseconds * 1000));
        }
    }
}
