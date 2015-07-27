using System;
using System.IO;
using System.Linq;
using System.Reflection;
using HAL_Base;
using NetworkTables;
using WPILib.Internal;
using static HAL_Base.HAL;

namespace WPILib
{
    /// <summary>
    /// This is the base for all FRC Robots.
    /// </summary>
    /// <remarks>You must call the Main function from your robots Entrance Point.
    /// Note that if you use the templates, this will automatically be done for you.</remarks>
    public abstract class RobotBase : IDisposable
    {
        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// This holds the <see cref="DriverStation"/> object for this robot.
        /// </summary>
        protected readonly DriverStation m_ds;

        /// <summary>
        /// Creates a new RobotBase. When this is called, it initializes NetworkTables and the
        /// <see cref="DriverStation"/>
        /// </summary>
        protected RobotBase()
        {
            NetworkTable.SetServerMode();
            m_ds = DriverStation.Instance;
            NetworkTable.GetTable("");
            NetworkTable.GetTable("LiveWindow").GetSubTable("~STATUS~").PutBoolean("LW Enabled", false);
        }

        ///<inheritdoc/>
        public virtual void Dispose()
        {
        }

        /// Gets if the robot is in simulation
        public static bool IsSimulation => HALType == HALTypes.Simulation;

        /// Gets if the robot is running on a RoboRIO
        public static bool IsReal => !IsSimulation;

        /// Gets if the robot is disabled
        public bool IsDisabled => m_ds.Disabled;

        /// Gets if the robot is enabled
        public bool IsEnabled => m_ds.Enabled;

        /// Gets if the robot is in Autonomous
        public bool IsAutonomous => m_ds.Autonomous;

        /// Gets if the robot is in Test
        public bool IsTest => m_ds.Test;

        /// Gets if the robot is in Operator Control
        public bool IsOperatorControl => m_ds.OperatorControl;

        /// Gets if new control data is available
        public bool IsNewDataAvailable => m_ds.NewControlData;

        /// <summary>
        /// This function is called by the initializer to start the main loop.
        /// </summary>
        public abstract void StartCompetition();

        /// <summary>
        /// This function is called right before <see cref="StartCompetition"/>
        /// </summary>
        protected virtual void Prestart()
        {
            HALNetworkCommunicationObserveUserProgramStarting();
        }

        /// <summary>
        /// Common initialization for all robot programs.
        /// </summary>
        public static void InitializeHardwareConfiguration()
        {
            Initialize();
            RobotState.Implementation = DriverStation.Instance;
            Timer.Implementation = new HardwareTimer();
            HLUsageReporting.Implementation = new HardwareHLUsageReporting();
        }

        private static RobotBase s_robot;
        /// <summary>
        /// Starting point for robot applications. You can provide either an assembly, or a type. 
        /// If passed a type, it takes priority.
        /// </summary>
        /// <param name="robotAssembly">The assembly the main robot class is located in.</param>
        /// <param name="robotType">The main robot class type</param>
        public static void Main(Assembly robotAssembly, Type robotType = null)
        {
            try
            {
                InitializeHardwareConfiguration();
            }
            catch (Exception e)
            {
                Console.WriteLine("Static Robot Initialization Failed.");
                Console.WriteLine(e);
                Environment.Exit(1);
            }
            Report(ResourceType.kResourceType_Language, Instances.kLanguage_DotNet);
            string robotName = "";
            string robotAssemblyName = "";
            //Find the robot code class
            try
            {
                //If we were passed an assembly but not a type
                if (robotType == null && robotAssembly != null)
                {
                    //Load the first non abstract class inheriting from RobotBase we can find.
                    robotAssemblyName = robotAssembly.GetName().Name;
                    var robotClasses =
                        (from t in robotAssembly.GetTypes()
                         where typeof (RobotBase).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface
                         select t)
                            .ToList();
                    if (robotClasses.Count == 0)
                        throw new Exception(
                            "Could not find base robot class. Are you sure the assembly got passed correctly to the main function?");
                    robotName = robotClasses[0].FullName;
                    s_robot = (RobotBase) (Activator.CreateInstance(robotAssemblyName, robotName)).Unwrap();
                }
                else
                {
                    //If not type or assembly throw an exception
                    if (robotType == null)
                    {
                        throw new Exception("Both robotAssembly and robotType cannot be null.");
                    }
                    //Otherwise just initialize the type we were passed.
                    s_robot = (RobotBase) (Activator.CreateInstance(robotType));
                }
                s_robot.Prestart();
            }
            catch (Exception ex)
            {
                DriverStation.ReportError("ERROR Unhandled exception instantiating robot " + robotName + " " + ex + " at " + ex.StackTrace, false);
                Console.Error.WriteLine("WARNING: Robots don't quit!");
                Console.Error.WriteLine("Error: Could not instantiate robot " + robotName + "!");
                Environment.Exit(1);
                return;
            }

            //Write to the version file.
            if (HALType == HALTypes.RoboRIO)
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
            bool errorOnExit = false;
            try
            {
                s_robot.StartCompetition();
            }
            catch (Exception ex)
            {
                DriverStation.ReportError("ERROR Unhandled exception" + ex, false);
                errorOnExit = true;
            }
            finally
            {
                Console.Error.WriteLine("WARNING: Robots don't quit!");
                if (errorOnExit)
                {
                    Console.Error.WriteLine("---> The StartCompetition() method (or methods called by it) should have handled the exception above.");
                }
                else
                {
                    Console.Error.WriteLine("---> Unexpected return fom the StartCompetition() method.");
                }
                DriverStation.ReportError("ERROR StartCompetition() returned", false);
                Environment.Exit(1);
            }
            
        }
    }
}
