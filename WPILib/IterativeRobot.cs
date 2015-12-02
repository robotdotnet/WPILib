using HAL;
using HAL.Base;
using WPILib.LiveWindows;
using static HAL.Base.HAL;
using static System.Console;

namespace WPILib
{
    /// <summary>
    /// Implements a iterative robot framework, extending from <see cref="RobotBase"/>.
    /// </summary>
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

        /// <summary>
        /// Provide an alternate "main loop" via startCompetition().
        /// </summary>
        public override void StartCompetition()
        {
            Report(ResourceType.kResourceType_Framework, Instances.kFramework_Iterative);

            RobotInit();

            // Tell the DS that the robot is ready to be enabled.
            HALNetworkCommunicationObserveUserProgramStarting();

            LiveWindow.LiveWindow.SetEnabled(false);
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
                        LiveWindow.LiveWindow.SetEnabled(false);
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
                        LiveWindow.LiveWindow.SetEnabled(true);
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
                        LiveWindow.LiveWindow.SetEnabled(false);
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
                        LiveWindow.LiveWindow.SetEnabled(false);
                        TeleopInit();
                        m_teleopInitialized = true;
                        m_testInitialized = false;
                        m_autonomousInitialized = false;
                        m_disabledInitialized = false;
                    }
                    if (NextPeriodReady)
                    {
                        HALNetworkCommunicationObserveUserProgramTeleop();
                        TeleopPeriodic();
                    }
                }
                m_ds.WaitForData();
            }
// ReSharper disable once FunctionNeverReturns
        }

        private bool NextPeriodReady => m_ds.NewControlData;

        /// <summary>
        /// Robot-wide initialization code should go here.
        /// </summary>
        /// <remarks>
        /// Users should override this method for default Robot-wide initialiation which will be called
        /// when the robot is first powered on. It will be called exactly one time.
        /// <para></para>
        /// Warning: The Driver Station "Robot Code" light and FMS "Robot Ready" indicators will be off until
        /// <see cref="RobotInit"/> exits. Code in <see cref="RobotInit"/> that waits for enable will cause
        /// the robot to never indicate that the code is ready, causing the robot to be bypassed in a match.
        /// </remarks>
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
