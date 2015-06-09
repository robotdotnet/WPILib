﻿using HAL_Base;
using static HAL_Base.HAL;
using static System.Console;

namespace WPILib
{
    public class IterativeRobot : RobotBase
    {
        private bool m_disabledInitialized;
        private bool m_autonomousInitialized;
        private bool m_teleopInitialized;
        private bool m_testInitialized;

        public IterativeRobot()
        {
            m_autonomousInitialized = false;
            m_disabledInitialized = false;
            m_teleopInitialized = false;
            m_testInitialized = false;
        }

        protected override void Prestart()
        {
        }

        /// <summary>
        /// Provide an alternate "main loop" via startCompetition().
        /// </summary>
        public override void StartCompetition()
        {
            Report(ResourceType.kResourceType_Framework, Instances.kFramework_Iterative);

            RobotInit();

            HALNetworkCommunicationObserveUserProgramStarting();

            //LiveWindow.setEnabled(false);
            while (true)
            {
                //Console.WriteLine("RobotLoop");
                // Call the appropriate function depending upon the current robot mode
                if (IsDisabled)
                {
                    // call DisabledInit() if we are now just entering disabled mode from
                    // either a different mode or from power-on
                    if (!m_disabledInitialized)
                    {
                        //LiveWindow.setEnabled(false);
                        DisabledInit();
                        m_disabledInitialized = true;
                        // reset the initialization flags for the other modes
                        m_autonomousInitialized = false;
                        m_teleopInitialized = false;
                        m_testInitialized = false;
                    }
                    if (NextPeriodReady)
                    {
                        HALNetworkCommunicationObserveUserProgramDisabled();
                        DisabledPeriodic();
                    }
                }
                else if (IsTest)
                {
                    // call TestInit() if we are now just entering test mode from either
                    // a different mode or from power-on
                    if (!m_testInitialized)
                    {
                        //LiveWindow.setEnabled(true);
                        TestInit();
                        m_testInitialized = true;
                        m_autonomousInitialized = false;
                        m_teleopInitialized = false;
                        m_disabledInitialized = false;
                    }
                    if (NextPeriodReady)
                    {
                        HALNetworkCommunicationObserveUserProgramTest();
                        TestPeriodic();
                    }
                }
                else if (IsAutonomous)
                {
                    // call Autonomous_Init() if this is the first time
                    // we've entered autonomous_mode
                    if (!m_autonomousInitialized)
                    {
                        //LiveWindow.setEnabled(false);
                        // KBS NOTE: old code reset all PWMs and relays to "safe values"
                        // whenever entering autonomous mode, before calling
                        // "Autonomous_Init()"
                        AutonomousInit();
                        m_autonomousInitialized = true;
                        m_testInitialized = false;
                        m_teleopInitialized = false;
                        m_disabledInitialized = false;
                    }
                    if (NextPeriodReady)
                    {
                        HALNetworkCommunicationObserveUserProgramAutonomous();
                        AutonomousPeriodic();
                    }
                }
                else
                {
                    // call Teleop_Init() if this is the first time
                    // we've entered teleop_mode
                    if (!m_teleopInitialized)
                    {
                        //LiveWindow.setEnabled(false);
                        TeleopInit();
                        m_teleopInitialized = true;
                        m_testInitialized = false;
                        m_autonomousInitialized = false;
                        m_disabledInitialized = false;
                    }
                    if (NextPeriodReady)
                    {
                        //HAL.NetworkCommunicationObserveUserProgramTeleop();
                        HALNetworkCommunicationObserveUserProgramTeleop();
                        TeleopPeriodic();
                    }
                }
                m_ds.WaitForData();
            }
// ReSharper disable once FunctionNeverReturns
        }

        private bool NextPeriodReady => m_ds.NewControlData;

        public virtual void RobotInit()
        {
            WriteLine($"Default {nameof(IterativeRobot)}.{nameof(RobotInit)} method... Overload me!");
        }

        public virtual void DisabledInit()
        {
            WriteLine($"Default {nameof(IterativeRobot)}.{nameof(DisabledInit)} method... Overload me!");
        }

        public virtual void TestInit()
        {
            WriteLine($"Default {nameof(IterativeRobot)}.{nameof(TestInit)} method... Overload me!");
        }

        public virtual void AutonomousInit()
        {
            WriteLine($"Default {nameof(IterativeRobot)}.{nameof(AutonomousInit)} method... Overload me!");
        }

        public virtual void TeleopInit()
        {
            WriteLine($"Default {nameof(IterativeRobot)}.{nameof(TeleopInit)} method... Overload me!");
        }

        private bool dpFirstRun = true;

        public virtual void DisabledPeriodic()
        {
            if (dpFirstRun)
            {
                WriteLine($"Default {nameof(IterativeRobot)}.{nameof(DisabledPeriodic)} method... Overload me!");
                dpFirstRun = false;
            }
            Timer.Delay(0.001);
        }

        private bool apFirstRun = true;

        public virtual void AutonomousPeriodic()
        {
            if (apFirstRun)
            {
                WriteLine($"Default {nameof(IterativeRobot)}.{nameof(AutonomousPeriodic)} method... Overload me!");
                apFirstRun = false;
            }
            Timer.Delay(0.001);
        }

        private bool tpFirstRun = true;

        public virtual void TeleopPeriodic()
        {
            if (tpFirstRun)
            {
                WriteLine($"Default {nameof(IterativeRobot)}.{nameof(TeleopPeriodic)} method... Overload me!");
                tpFirstRun = false;
            }
            Timer.Delay(0.001);
        }

        private bool tmpFirstRun = true;

        public virtual void TestPeriodic()
        {
            if (tmpFirstRun)
            {
                WriteLine($"Default {nameof(IterativeRobot)}.{nameof(TestPeriodic)} method... Overload me!");
                tmpFirstRun = false;
            }
        }
    }
}
