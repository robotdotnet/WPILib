using System;
using System.Collections.Generic;
using System.Text;
using HAL_RoboRIO;
using System.IO;

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

        public static void main(string RobotName)
        {
            InitializeHardwareConfiguration();
            HAL.Report(ResourceType.kResourceType_Language, Instances.kLanguage_Python);

            RobotBase robot;
            try
            {
                robot = (RobotBase)(System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(RobotName));
                robot.Prestart();
            }
            catch (Exception ex)
            {

                DriverStation.ReportError("EROR Unhandled exception instantiating robot " + RobotName + " " + ex.ToString() + " at " + ex.StackTrace, false);//"ERROR Could not instantiate robot", true);
                //Log Robots dont quit
                Console.Error.WriteLine("WARNING: Robots don't quit!");
                Console.Error.WriteLine("Error: Could not instantiate robot " + RobotName + "!");
                Environment.Exit(1);
                return;
            }

            string file = "/tmp/frc_versions/FRC_Lib_Version.ini";
            try
            {
                if (File.Exists(file))
                    File.Delete(file);

                File.WriteAllText(file, "RobotDotNet V1.0");
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.StackTrace);
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
