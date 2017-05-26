using HAL.Base;
using static HAL.Base.HALDriverStation;
using static HAL.Base.HAL;
using static System.Console;

namespace WPILib
{
    /// <summary>
    /// Implements a iterative robot framework, extending from <see cref="RobotBase"/>.
    /// </summary>
    /// <remarks>
    /// IterativeRobot implements a specific type of Robot Program framework,
    /// extending the RobotBase class.
    /// <para/>
    /// The IterativeRobot class is intended to be subclassed by a user creating a
    /// robot program.
    ///<para/>
    /// This class is intended to implement the "old style" default code, by
    /// providing the following functions which are called by the main loop,
    /// startCompetition(), at the appropriate times:
    ///<para/>
    /// robotInit() -- provide for initialization at robot power-on
    ///<para/>
    /// init() functions -- each of the following functions is called once when the
    /// appropriate mode is entered: - DisabledInit() -- called only when first
    /// disabled - AutonomousInit() -- called each and every time autonomous is
    /// entered from another mode - TeleopInit() -- called each and every time teleop
    /// is entered from another mode - TestInit() -- called each and every time test
    /// mode is entered from anothermode
    ///<para/>
    /// Periodic() functions -- each of these functions is called iteratively at the
    /// appropriate periodic rate(aka the "slow loop"). The period of the iterative
    /// robot is synced to the driver station control packets, giving a periodic
    /// frequency of about 50Hz(50 times per second). - disabledPeriodic() -
    /// autonomousPeriodic() - teleopPeriodic() - testPeriodoc()
    /// </remarks>
    public class IterativeRobot : RobotBase
    {
        private bool m_disabledInitialized;
        private bool m_autonomousInitialized;
        private bool m_teleopInitialized;
        private bool m_testInitialized;

        /// <summary>
        /// Constructor for IterativeRobot.
        /// </summary>
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
            HAL_ObserveUserProgramStarting();

            LiveWindow.LiveWindow.SetEnabled(false);
            while (true)
            {
                // Wait for new data to arrive
                m_ds.WaitForData();
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
                    HAL_ObserveUserProgramDisabled();
                    DisabledPeriodic();
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
                    HAL_ObserveUserProgramDisabled();
                    TestPeriodic();
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
                    HAL_ObserveUserProgramAutonomous();
                    AutonomousPeriodic();
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
                    HAL_ObserveUserProgramTeleop();
                    TeleopPeriodic();
                }
                RobotPeriodic();
            }
            // ReSharper disable once FunctionNeverReturns
        }

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

        /// <summary>
        /// Initialization code for disabled mode should go here.
        /// </summary>
        public virtual void DisabledInit()
        {
            WriteLine($"Default {nameof(IterativeRobot)}.{nameof(DisabledInit)} method... Overload me!");
        }

        /// <summary>
        /// Initialization code for test mode should go here.
        /// </summary>
        public virtual void TestInit()
        {
            WriteLine($"Default {nameof(IterativeRobot)}.{nameof(TestInit)} method... Overload me!");
        }

        /// <summary>
        /// Initialization code for autonomous mode should go here.
        /// </summary>
        public virtual void AutonomousInit()
        {
            WriteLine($"Default {nameof(IterativeRobot)}.{nameof(AutonomousInit)} method... Overload me!");
        }

        /// <summary>
        /// Initialization for teleop mode should go here.
        /// </summary>
        public virtual void TeleopInit()
        {
            WriteLine($"Default {nameof(IterativeRobot)}.{nameof(TeleopInit)} method... Overload me!");
        }

        private bool m_dpFirstRun = true;

        /// <summary>
        /// Periodic code for disabled mode should go here.
        /// </summary>
        public virtual void DisabledPeriodic()
        {
            if (m_dpFirstRun)
            {
                WriteLine($"Default {nameof(IterativeRobot)}.{nameof(DisabledPeriodic)} method... Overload me!");
                m_dpFirstRun = false;
            }
            Timer.Delay(0.001);
        }

        private bool m_apFirstRun = true;

        /// <summary>
        /// Periodic code for autonomous mode should go here.
        /// </summary>
        public virtual void AutonomousPeriodic()
        {
            if (m_apFirstRun)
            {
                WriteLine($"Default {nameof(IterativeRobot)}.{nameof(AutonomousPeriodic)} method... Overload me!");
                m_apFirstRun = false;
            }
            Timer.Delay(0.001);
        }

        private bool m_tpFirstRun = true;

        /// <summary>
        /// Periodic code for teleop mode should go here.
        /// </summary>
        public virtual void TeleopPeriodic()
        {
            if (m_tpFirstRun)
            {
                WriteLine($"Default {nameof(IterativeRobot)}.{nameof(TeleopPeriodic)} method... Overload me!");
                m_tpFirstRun = false;
            }
            Timer.Delay(0.001);
        }

        private bool m_tmpFirstRun = true;

        /// <summary>
        /// Periodic code for test mose should go here.
        /// </summary>
        public virtual void TestPeriodic()
        {
            if (m_tmpFirstRun)
            {
                WriteLine($"Default {nameof(IterativeRobot)}.{nameof(TestPeriodic)} method... Overload me!");
                m_tmpFirstRun = false;
            }
        }

        private bool m_rpFirstFun = true;

        /// <summary>
        /// Periodic code for all modes should go here.
        /// </summary>
        public virtual void RobotPeriodic()
        {
            if (m_rpFirstFun)
            {
                WriteLine($"Default {nameof(IterativeRobot)}.{nameof(RobotPeriodic)} method... Overload me!");
                m_rpFirstFun = false;
            }
            Timer.Delay(0.001);
        }
    }
}
