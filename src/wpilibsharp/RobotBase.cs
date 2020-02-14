using Hal;
using NetworkTables;
using System;
using System.IO;
using System.Threading;

namespace WPILib
{
    public abstract class RobotBase : IDisposable
    {
        private static void RunRobot<Robot>() where Robot : RobotBase, new()
        {
            Console.WriteLine("\n********** Robot program starting **********");

            Robot robot;
            try
            {
                robot = new Robot();
            }
            catch (Exception ex)
            {
                string robotName = typeof(Robot).Name;
                DriverStation.ReportError(("Unhandled exception instanciating robot "
                    + robotName + " " + ex.Message), ex.StackTrace);
                DriverStation.ReportWarning("Robots should not quit, but yours did!", false);
                DriverStation.ReportError("Could not instanciate robot " + robotName + "!", false);
                return;
            }

            lock (m_runMutex)
            {
                m_robotCopy = robot;
            }

            if (IsReal)
            {
                try
                {
                    File.WriteAllText("/tmp/frc_versions/FRC_Lib_Version.ini", $"C# {WPILibVersion}");
                }
                catch (Exception ex)
                {
                    DriverStation.ReportError("Could not write FRC_Lib_Version.ini: " + ex.Message, ex.StackTrace);
                }
            }

            bool errorOnExit = false;
            try
            {
                robot.StartCompetition();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                DriverStation.ReportError("Unhandled Exception: " + ex.Message, ex.StackTrace);
                errorOnExit = true;
            }
            finally
            {
                bool suppressExitWarningLocal;
                lock (m_runMutex)
                {
                    suppressExitWarningLocal = suppressExitWarning;
                }

                if (!suppressExitWarningLocal)
                {
                    DriverStation.ReportWarning("Robots should not quit, but yours did!", false);
                    if (errorOnExit)
                    {
                        DriverStation.ReportError("The StartCompetition() method (or methods called by it) should have handled the exception above.", false);
                    }
                    else
                    {
                        DriverStation.ReportError("Unexpected return from StartCompetition() method.", false);
                    }
                }
            }
        }

        private static bool suppressExitWarning = false;

        private static int RunHALInitialization()
        {
            if (!Hal.HALLowLevel.Initialize())
            {
                Console.WriteLine("FATAL ERROR: HAL could not be initialized");
                return -1;
            }

            NetworkTables.Natives.NtCore.Initialize();

            return 0;
        }

        private static readonly object m_runMutex = new object();
        private static RobotBase? m_robotCopy;
        public static void SuppressExitWarning(bool value)
        {
            lock (m_runMutex)
            {
                suppressExitWarning = true;
            }
        }

        public static void StartRobot<Robot>() where Robot : RobotBase, new()
        {
            int halInit = RunHALInitialization();
            if (halInit != 0)
            {
                throw new InvalidOperationException("Base Robot Functionality Failed to Initialize. Terminating");
            }

            UsageReporting.Report(ResourceType.Language, Instances.Language_DotNet, 0, WPILibVersion);

            if (MainLowLevel.HasMain())
            {
                Thread thread = new Thread(() =>
                {
                    RunRobot<Robot>();
                    MainLowLevel.ExitMain();
                })
                {
                    Name = "Robot Main",
                    IsBackground = true
                };
                thread.Start();
                MainLowLevel.RunMain();
                SuppressExitWarning(true);
                RobotBase? robot;
                lock (m_runMutex)
                {
                    robot = m_robotCopy;
                }

                robot?.EndCompetition();
                if (thread.Join(TimeSpan.FromSeconds(1)))
                {
                    robot?.Dispose();
                }
            }
            else
            {
                RunRobot<Robot>();
            }


        }

        protected DriverStation m_ds;

        protected static int m_threadId;

        public bool IsEnabled => m_ds.IsEnabled;
        public bool IsDisabled => m_ds.IsDisabled;
        public bool IsAutonomous => m_ds.IsAutonomous;
        public bool IsOperatorControl => m_ds.IsOperatorControl;
        public bool IsTest => m_ds.IsTest;

        public bool IsNewDataAvailable => m_ds.IsNewControlData;

        public static int MainThreadId => m_threadId;

        public static string WPILibVersion => "1234";

        public abstract void StartCompetition();

        public abstract void EndCompetition();

        public virtual void Dispose()
        {

        }

        public static bool IsReal => Hal.HALLowLevel.GetRuntimeType() == Hal.RuntimeType.Athena;
        public static bool IsSimulation => !IsReal;

        public RobotBase()
        {
            m_threadId = Thread.CurrentThread.ManagedThreadId;

            var inst = NetworkTableInstance.Default;
            inst.SetNetworkIdentity("Robot");


            if (IsReal)
            {
                inst.StartServer("/home/lvuser/networktables.ini");
            }
            else
            {
                inst.StartServer();
            }

            inst.GetTable("LiveWindow")
                .GetSubTable(".status")
                .GetEntry("LW Enabled")
                .SetBoolean(false);

            m_ds = DriverStation.Instance;
            LiveWindow.LiveWindow.Enabled = false;

        }

    }
}
