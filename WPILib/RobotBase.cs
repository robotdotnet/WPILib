

using System;
using System.IO;
using System.Linq;
using HAL_Base;

namespace WPILib
{
    public abstract class RobotBase
    {
        public const int RobotTaskPriority = 101;

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
        public static bool IsSimulation()
        {
            return false;
        }

        /**
         * @return If the robot is running in the real world.
         */
        public static bool IsReal()
        {
            return true;
        }

        /**
         * Determine if the Robot is currently disabled.
         * @return True if the Robot is currently disabled by the field controls.
         */
        public bool IsDisabled()
        {
            return m_ds.IsDisabled();
        }

        /**
         * Determine if the Robot is currently enabled.
         * @return True if the Robot is currently enabled by the field controls.
         */
        public bool IsEnabled()
        {
            return m_ds.IsEnabled();
        }

        /**
         * Determine if the robot is currently in Autonomous mode.
         * @return True if the robot is currently operating Autonomously as determined by the field controls.
         */
        public bool IsAutonomous()
        {
            return m_ds.IsAutonomous();
        }

        /**
         * Determine if the robot is currently in Test mode
         * @return True if the robot is currently operating in Test mode as determined by the driver station.
         */
        public bool IsTest()
        {
            return m_ds.IsTest();
        }

        /**
         * Determine if the robot is currently in Operator Control mode.
         * @return True if the robot is currently operating in Tele-Op mode as determined by the field controls.
         */
        public bool IsOperatorControl()
        {
            return m_ds.IsOperatorControl();
        }

        /**
         * Indicates if new data is available from the driver station.
         * @return Has new data arrived over the network since the last time this function was called?
         */
        public bool IsNewDataAvailable()
        {
            return m_ds.IsNewControlData();
        }

        public abstract void StartCompetition();

        protected virtual void Prestart()
        {
            HAL.HALNetworkCommunicationObserveUserProgramStarting();
        }

        public static void InitializeHardwareConfiguration()
        {
            //HAL.IsSimulation = false;
            HAL.Initialize();
        }

        private static RobotBase robot;
        public static void main(System.Reflection.Assembly robotAssembly)//, string robotName)
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                HAL.IsSimulation = false;
            }
            else
                HAL.IsSimulation = true;
            InitializeHardwareConfiguration();
            HAL.Report(ResourceType.kResourceType_Language, Instances.kLanguage_CPlusPlus);
            string robotName = "";
            string robotAssemblyName = "";
            try
            {
                robotAssemblyName = robotAssembly.GetName().Name;
                var robotClasses = from t in robotAssembly.GetTypes() where typeof(RobotBase).IsAssignableFrom(t) select t;
                if (robotClasses.ToList().Count == 0)
                    throw new Exception("Could not find base robot class. Are you sure the assembly got passed correctly to the main function?");
                robotName = robotClasses.ToList()[0].FullName;
                //var types = robotAssembly.GetTypes().Where(t => typeof(RobotBase).IsAssignableFrom(t));
                //var type = types[0];
                robot = (RobotBase)(Activator.CreateInstance(robotAssemblyName, robotName)).Unwrap();
                //robot = (RobotBase)(System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(RobotName));
                robot.Prestart();
            }
            catch (Exception ex)
            {
                DriverStation.ReportError("EROR Unhandled exception instantiating robot " + robotName + " " + ex.ToString() + " at " + ex.StackTrace, false);//"ERROR Could not instantiate robot", true);
                //Log Robots dont quit
                Console.Error.WriteLine("WARNING: Robots don't quit!");
                Console.Error.WriteLine("Error: Could not instantiate robot " + robotName + "!");
                Environment.Exit(1);
                return;
            }
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
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
