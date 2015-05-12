using System;
using System.Collections.Generic;
using System.Text;
using HAL_RoboRIO;

namespace WPILib
{
    public class IterativeRobot : RobotBase
    {
        private bool m_disabledInitialized;
        private bool m_autonomousInitialized;
        private bool m_teleopInitialized;
        private bool m_testInitialized;

        public IterativeRobot() : base()
        {
            m_autonomousInitialized = false;
            m_disabledInitialized = false;
            m_teleopInitialized = false;
            m_testInitialized = false;
        }

        protected override void Prestart()
        {
            
        }

        public override void StartCompetition()
        {
            HAL.Report(ResourceType.kResourceType_Framework, Instances.kFramework_Iterative);

            RobotInit();

            HAL.NetworkCommunicationObserveUserProgramStarting();

            //LiveWindow.setEnabled(false);
            while (true)
            {
                // Call the appropriate function depending upon the current robot mode
                if (isDisabled())
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
                    if (NextPeriodReady())
                    {
                        HAL.NetworkCommunicationObserveUserProgramDisabled();
                        DisabledPeriodic();
                    }
                }
                else if (isTest())
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
                    if (NextPeriodReady())
                    {
                        HAL.NetworkCommunicationObserveUserProgramTest();
                        TestPeriodic();
                    }
                }
                else if (isAutonomous())
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
                    if (NextPeriodReady())
                    {
                        HAL.NetworkCommunicationObserveUserProgramAutonomous();
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
                    if (NextPeriodReady())
                    {
                        //HAL.NetworkCommunicationObserveUserProgramTeleop();
                        HAL.NetworkCommunicationObserveUserProgramTeleop();
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
