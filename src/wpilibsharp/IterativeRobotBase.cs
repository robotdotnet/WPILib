using System;
using System.Collections.Generic;
using System.Text;

namespace WPILib
{
    public abstract class IterativeRobotBase : RobotBase
    {
        public virtual void RobotInit()
        {

        }

        public virtual void DisabledInit()
        {

        }

        public virtual void AutonomousInit()
        {

        }

        public virtual void TeleopInit()
        {

        }

        public virtual void TestInit()
        {

        }

        public virtual void RobotPeriodic()
        {

        }

        public virtual void DisabledPeriodic()
        {

        }

        public virtual void AutonomousPeriodic()
        {

        }

        public virtual void TeleopPeriodic()
        {

        }

        public virtual void TestPeriodic()
        {

        }

        public IterativeRobotBase(TimeSpan period)
        {
            m_period = period;
            m_watchdog = new Watchdog(period, PrintLoopOverrunMessage);
        }

        protected void LoopFunc()
        {
            m_watchdog.Reset();


            if (IsDisabled)
            {
                if (m_lastMode != Mode.Disabled)
                {
                    DisabledInit();
                    m_watchdog.AddEpoch("DisabledInit()");
                    m_lastMode = Mode.Disabled;
                }

                Hal.DriverStation.ObserveUserProgramDisabled();
                DisabledPeriodic();
                m_watchdog.AddEpoch("DisabledPeriodic()");
            }
            else if (IsAutonomous)
            {
                if (m_lastMode != Mode.Autonomous)
                {
                    AutonomousInit();
                    m_watchdog.AddEpoch("AutonomousInit()");
                    m_lastMode = Mode.Autonomous;
                }

                Hal.DriverStation.ObserveUserProgramAutonomous();
                AutonomousPeriodic();
                m_watchdog.AddEpoch("AutonomousPeriodic()");
            }
            else if (IsOperatorControl)
            {
                if (m_lastMode != Mode.Teleop)
                {
                    TeleopInit();
                    m_watchdog.AddEpoch("TeleopInit()");
                    m_lastMode = Mode.Teleop;
                }

                Hal.DriverStation.ObserveUserProgramTeleop();
                TeleopPeriodic();
                m_watchdog.AddEpoch("TeleopPeriodic()");
            } else
            {
                if (m_lastMode != Mode.Test)
                {
                    TestInit();
                    m_watchdog.AddEpoch("TestInit()");
                    m_lastMode = Mode.Autonomous;
                }

                Hal.DriverStation.ObserveUserProgramAutonomous();
                TestPeriodic();
                m_watchdog.AddEpoch("TestPeriodic()");
            }

            RobotPeriodic();
            m_watchdog.AddEpoch("RobotPeriodic()");

            m_watchdog.Disable();

            if (m_watchdog.IsExpired)
            {
                m_watchdog.PrintEpochs();
            }
        }

        protected TimeSpan m_period;

        private enum Mode
        {
            None,
            Disabled,
            Autonomous,
            Teleop,
            Test
        }

        private Mode m_lastMode = Mode.None;
        private readonly Watchdog m_watchdog;

        private void PrintLoopOverrunMessage()
        {
            DriverStation.ReportWarning($"Loop time of {m_period}s overrun\n".AsSpan());
        }
    }
}
