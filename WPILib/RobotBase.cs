using System;
using System.IO;
using System.Linq;
using System.Reflection;
using HAL_Base;
using NetworkTablesDotNet.NetworkTables;
using WPILib.Internal;
using static HAL_Base.HAL;

namespace WPILib
{
    public abstract class RobotBase : IDisposable
    {
        public const int RobotTaskPriority = 101;

// ReSharper disable once InconsistentNaming
        protected readonly DriverStation m_ds;

        protected RobotBase()
        {
            NetworkTable.SetServerMode();
            m_ds = DriverStation.Instance;
            NetworkTable.GetTable("");
            NetworkTable.GetTable("LiveWindow").GetSubTable("~STATUS~").PutBoolean("LW Enabled", false);
        }

        public void Dispose()
        {
        }

        public static bool IsSimulation => false;

        public static bool IsReal => true;

        public bool IsDisabled => m_ds.Disabled;

        public bool IsEnabled => m_ds.Enabled;

        public bool IsAutonomous => m_ds.Autonomous;

        public bool IsTest => m_ds.Test;

        public bool IsOperatorControl => m_ds.OperatorControl;

        public bool IsNewDataAvailable => m_ds.NewControlData;


        public abstract void StartCompetition();

        protected virtual void Prestart()
        {
            HALNetworkCommunicationObserveUserProgramStarting();
        }

        public static void InitializeHardwareConfiguration()
        {
            Initialize();
            RobotState.Implementation = DriverStation.Instance;
            Timer.Implementation = new HardwareTimer();
            HLUsageReporting.Implementation = new HardwareHLUsageReporting();

        }

        private static RobotBase s_robot;
        public static void Main(Assembly robotAssembly)
        {
            HAL.IsSimulation = Environment.OSVersion.Platform != PlatformID.Unix;
            InitializeHardwareConfiguration();
            Report(ResourceType.kResourceType_Language, Instances.kLanguage_CPlusPlus);
            string robotName = "";
            string robotAssemblyName = "";
            try
            {
                robotAssemblyName = robotAssembly.GetName().Name;
                var robotClasses = (from t in robotAssembly.GetTypes() where typeof(RobotBase).IsAssignableFrom(t) select t).ToList();
                if (robotClasses.Count == 0)
                    throw new Exception("Could not find base robot class. Are you sure the assembly got passed correctly to the main function?");
                robotName = robotClasses[0].FullName;
                s_robot = (RobotBase)(Activator.CreateInstance(robotAssemblyName, robotName)).Unwrap();
                s_robot.Prestart();
            }
            catch (Exception ex)
            {
                DriverStation.ReportError("ERROR Unhandled exception instantiating robot " + robotName + " " + ex.ToString() + " at " + ex.StackTrace, false);//"ERROR Could not instantiate robot", true);
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
                    var version = Assembly.GetExecutingAssembly().GetName().Version;
                    File.WriteAllText(file, "RobotDotNet V" + version);
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }

            try
            {
                s_robot.StartCompetition();
            }
            //Add a keyboard exception
            catch (Exception ex)
            {
                DriverStation.ReportError("ERROR Unhandled exception" + ex.ToString(), false);
                return;
            }
            finally
            {
                DriverStation.ReportError("ERROR StartCompetition() returned", false);
                Environment.Exit(1);
            }
            
        }
    }
}
