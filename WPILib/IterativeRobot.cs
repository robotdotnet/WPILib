

using System;
using HAL_Base;

namespace WPILib
{
    public class IterativeRobot : RobotBase
    {
        private bool _disabledInitialized;
        private bool _autonomousInitialized;
        private bool _teleopInitialized;
        private bool _testInitialized;

        public IterativeRobot() : base()
        {
            _autonomousInitialized = false;
            _disabledInitialized = false;
            _teleopInitialized = false;
            _testInitialized = false;
        }

        protected override void Prestart()
        {
        }

        public void ObserveStarting()
        {
            HAL.HALNetworkCommunicationObserveUserProgramStarting();
        }

        public override void StartCompetition()
        {
            HAL.Report(ResourceType.kResourceType_Framework, Instances.kFramework_Iterative);

            RobotInit();

            HAL.HALNetworkCommunicationObserveUserProgramStarting();

            //LiveWindow.setEnabled(false);
            while (true)
            {
                //Console.WriteLine("RobotLoop");
                // Call the appropriate function depending upon the current robot mode
                if (IsDisabled())
                {
                    // call DisabledInit() if we are now just entering disabled mode from
                    // either a different mode or from power-on
                    if (!_disabledInitialized)
                    {
                        //LiveWindow.setEnabled(false);
                        DisabledInit();
                        _disabledInitialized = true;
                        // reset the initialization flags for the other modes
                        _autonomousInitialized = false;
                        _teleopInitialized = false;
                        _testInitialized = false;
                    }
                    if (NextPeriodReady())
                    {
                        HAL.HALNetworkCommunicationObserveUserProgramDisabled();
                        DisabledPeriodic();
                    }
                }
                else if (IsTest())
                {
                    // call TestInit() if we are now just entering test mode from either
                    // a different mode or from power-on
                    if (!_testInitialized)
                    {
                        //LiveWindow.setEnabled(true);
                        TestInit();
                        _testInitialized = true;
                        _autonomousInitialized = false;
                        _teleopInitialized = false;
                        _disabledInitialized = false;
                    }
                    if (NextPeriodReady())
                    {
                        HAL.HALNetworkCommunicationObserveUserProgramTest();
                        TestPeriodic();
                    }
                }
                else if (IsAutonomous())
                {
                    // call Autonomous_Init() if this is the first time
                    // we've entered autonomous_mode
                    if (!_autonomousInitialized)
                    {
                        //LiveWindow.setEnabled(false);
                        // KBS NOTE: old code reset all PWMs and relays to "safe values"
                        // whenever entering autonomous mode, before calling
                        // "Autonomous_Init()"
                        AutonomousInit();
                        _autonomousInitialized = true;
                        _testInitialized = false;
                        _teleopInitialized = false;
                        _disabledInitialized = false;
                    }
                    if (NextPeriodReady())
                    {
                        HAL.HALNetworkCommunicationObserveUserProgramAutonomous();
                        AutonomousPeriodic();
                    }
                }
                else
                {
                    // call Teleop_Init() if this is the first time
                    // we've entered teleop_mode
                    if (!_teleopInitialized)
                    {
                        //LiveWindow.setEnabled(false);
                        TeleopInit();
                        _teleopInitialized = true;
                        _testInitialized = false;
                        _autonomousInitialized = false;
                        _disabledInitialized = false;
                    }
                    if (NextPeriodReady())
                    {
                        //HAL.NetworkCommunicationObserveUserProgramTeleop();
                        HAL.HALNetworkCommunicationObserveUserProgramTeleop();
                        TeleopPeriodic();
                    }
                }
                m_ds.WaitForData();
                //m_ds.waitForData();
            }
        }

        private bool NextPeriodReady()
        {
            return m_ds.IsNewControlData();
        }

        public virtual void RobotInit()
        {
            Console.WriteLine("Default Init Running");
        }

        public virtual void DisabledInit()
        {
            //Console.WriteLine("Default Disabled Init Running");
        }

        public virtual void DisabledPeriodic()
        {
        }

        public virtual void TestInit()
        {
        }

        public virtual void TestPeriodic()
        {
        }

        public virtual void AutonomousInit()
        {
        }

        public virtual void AutonomousPeriodic()
        {
        }

        public virtual void TeleopInit()
        {
        }

        public virtual void TeleopPeriodic()
        {
        }
    }
}
