using System;
using System.Collections.Generic;
using System.Text;
using HAL_RoboRIO;

namespace WPILib
{
    public abstract class RobotBase
    {
        public const int ROBOT_TASK_PRIORITY = 101;

        protected readonly DriverStation m_ds;

        protected RobotBase()
        {
            m_ds = DriverStation.GetInstance();


        }

        public void Free()
        {

        }

        /**
     * @return If the robot is running in simulation.
     */
        public static bool isSimulation()
        {
            return false;
        }

        /**
         * @return If the robot is running in the real world.
         */
        public static bool isReal()
        {
            return true;
        }

        /**
         * Determine if the Robot is currently disabled.
         * @return True if the Robot is currently disabled by the field controls.
         */
        public bool isDisabled()
        {
            return m_ds.IsDisabled();
        }

        /**
         * Determine if the Robot is currently enabled.
         * @return True if the Robot is currently enabled by the field controls.
         */
        public bool isEnabled()
        {
            return m_ds.IsEnabled();
        }

        /**
         * Determine if the robot is currently in Autonomous mode.
         * @return True if the robot is currently operating Autonomously as determined by the field controls.
         */
        public bool isAutonomous()
        {
            return m_ds.IsAutonomous();
        }

        /**
         * Determine if the robot is currently in Test mode
         * @return True if the robot is currently operating in Test mode as determined by the driver station.
         */
        public bool isTest()
        {
            return m_ds.IsTest();
        }

        /**
         * Determine if the robot is currently in Operator Control mode.
         * @return True if the robot is currently operating in Tele-Op mode as determined by the field controls.
         */
        public bool isOperatorControl()
        {
            return m_ds.IsOperatorControl();
        }

        /**
         * Indicates if new data is available from the driver station.
         * @return Has new data arrived over the network since the last time this function was called?
         */
        public bool isNewDataAvailable()
        {
            return m_ds.IsNewControlData();
        }

        public static bool IsSimulation()
        {
            return false;
        }

        public static bool IsReal()
        {
            return true;
        }

        public abstract void StartCompetition();

        protected virtual void Prestart()
        {
            HAL.NetworkCommunicationObserveUserProgramStarting();
        }

        public static void InitializeHardwareConfiguration()
        {
            HAL.Initialize();
        }

        public static void main(RobotBase rBase)
        {
            RobotBase.InitializeHardwareConfiguration();
            HAL.Report(ResourceType.kResourceType_Language, Instances.kLanguage_CPlusPlus);

            RobotBase robot;
            try
            {
                robot = rBase;
                robot.Prestart();
            }
            catch (Exception ex)
            {

                DriverStation.ReportError("ERROR Could not instantiate robot", true);
                //Log Robots dont quit
                return;
            }

            try
            {
                robot.StartCompetition();
            }
            //Add a keyboard exception
            catch (Exception ex)
            {
                DriverStation.ReportError("ERROR Unhandled exception", true);
                return;

            }
            finally
            {
                DriverStation.ReportError("ERROR StartCompetition() returned", false);
            }
        }
    }
}
