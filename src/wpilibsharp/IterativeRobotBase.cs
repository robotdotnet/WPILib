using System;
using System.Collections.Generic;
using System.Text;

namespace WPILib
{
    public abstract class IterativeRobotBase : RobotBase
    {
        public virtual void RobotInit()
        {
            Console.WriteLine("Default RobotInit() method... Override me!");
        }

        public virtual void DisabledInit()
        {
            Console.WriteLine("Default DisabledInit() method... Override me!");
        }

        public virtual void AutonomousInit()
        {
            Console.WriteLine("Default AutonomousInit() method... Override me!");
        }

        public virtual void TeleopInit()
        {
            Console.WriteLine("Default TeleopInit() method... Override me!");
        }

        public virtual void TestInit()
        {
            Console.WriteLine("Default TestInit() method... Override me!");
        }

        public virtual void RobotPeriodic()
        {
            if (m_rpFirstRun)
            {
                Console.WriteLine("Default RobotPeriodic() method... Override me!");
                m_rpFirstRun = false;
            }

        }

        private bool m_rpFirstRun = true;
        private bool m_dpFirstRun = true;
        private bool m_apFirstRun = true;
        private bool m_tpFirstRun = true;
        private bool m_tmpFirstRun = true;

        public virtual void DisabledPeriodic()
        {
            if (m_dpFirstRun)
            {
                Console.WriteLine("Default DisabledPeriodic() method... Override me!");
                m_dpFirstRun = false;
            }
        }

        public virtual void AutonomousPeriodic()
        {
            if (m_apFirstRun)
            {
                Console.WriteLine("Default AutonomousPeriodic() method... Override me!");
                m_apFirstRun = false;
            }
        }

        public virtual void TeleopPeriodic()
        {
            if (m_tpFirstRun)
            {
                Console.WriteLine("Default TeleopPeriodic() method... Override me!");
                m_tpFirstRun = false;
            }
        }

        public virtual void TestPeriodic()
        {
            if (m_tmpFirstRun)
            {
                Console.WriteLine("Default TestPeriodic() method... Override me!");
                m_tmpFirstRun = false;
            }
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
            DriverStation.ReportWarning($"Loop time of {m_period}s overrun\n", false);
        }
    }
}
