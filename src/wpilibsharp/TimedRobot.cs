using System;

namespace WPILib
{
    public class TimedRobot : IterativeRobotBase
    {
        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromMilliseconds(20);

        public override void StartCompetition()
        {
            RobotInit();

            Hal.DriverStationLowLevel.ObserveUserProgramStarting();

            m_expirationTime = Timer.FPGATimestamp + m_period;
            UpdateAlarm();

            while (true)
            {
                ulong curTime = Hal.NotifierLowLevel.WaitForAlarm(m_notifier, out var status);
                if (curTime == 0 || status != 0) break;

                m_expirationTime += m_period;

                UpdateAlarm();

                LoopFunc();
            }
        }

        public override void EndCompetition()
        {
            Hal.NotifierLowLevel.Stop(m_notifier);
        }

        public TimeSpan Period => m_period;

        public TimedRobot() : this(DefaultPeriod)
        {

        }

        public TimedRobot(TimeSpan period) : base(period)
        {
            m_notifier = Hal.NotifierLowLevel.Initialize();
            Hal.NotifierLowLevel.SetName(m_notifier, "TimedRobot");

            // Report
        }

        public override void Dispose()
        {
            Hal.NotifierLowLevel.Stop(m_notifier);
            Hal.NotifierLowLevel.Clean(m_notifier);

            base.Dispose();
        }

        private int m_notifier;
        private TimeSpan m_expirationTime;

        private void UpdateAlarm()
        {
            Hal.NotifierLowLevel.UpdateAlarm(m_notifier, (ulong)(m_expirationTime.TotalMilliseconds * 1000));
        }
    }
}
