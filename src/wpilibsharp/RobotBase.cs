using Hal;
using NetworkTables;
using System;
using System.IO;
using System.Threading;
using WPILib.LiveWindowNS;

namespace WPILib
{
#pragma warning disable CA1063 // Implement IDisposable Correctly
    public abstract class RobotBase : IDisposable
#pragma warning restore CA1063 // Implement IDisposable Correctly
    {
        private static void RunRobot<Robot>() where Robot : RobotBase, new()
        {
            Console.WriteLine("\n********** Robot program starting **********");

            Robot robot;
            try
            {
                robot = new Robot();
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
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
                catch (IOException ex)
                {
                    DriverStation.ReportError("Could not write FRC_Lib_Version.ini: " + ex.Message, ex.StackTrace);
                }
            }

            bool errorOnExit = false;
            try
            {
                robot.StartCompetition();
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
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
                suppressExitWarning = value;
            }
        }

        public static void StartRobot<TRobot>() where TRobot : RobotBase, new()
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
                    RunRobot<TRobot>();
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
                RunRobot<TRobot>();
            }


        }

        protected DriverStation DriverStation { get; }

        protected static int ThreadId { get; set; }

        public bool IsEnabled => DriverStation.IsEnabled;
        public bool IsDisabled => DriverStation.IsDisabled;
        public bool IsAutonomous => DriverStation.IsAutonomous;
        public bool IsOperatorControl => DriverStation.IsOperatorControl;
        public bool IsTest => DriverStation.IsTest;

        public bool IsNewDataAvailable => DriverStation.IsNewControlData;

        public static int MainThreadId => ThreadId;

        public static string WPILibVersion => "1234";

        public abstract void StartCompetition();

        public abstract void EndCompetition();

#pragma warning disable CA1063 // Implement IDisposable Correctly
        public virtual void Dispose()
#pragma warning restore CA1063 // Implement IDisposable Correctly
        {
            GC.SuppressFinalize(this);
        }

        public static bool IsReal => Hal.HALLowLevel.GetRuntimeType() == Hal.RuntimeType.Athena;
        public static bool IsSimulation => !IsReal;

        public RobotBase()
        {
            ThreadId = Thread.CurrentThread.ManagedThreadId;

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

            DriverStation = DriverStation.Instance;
            LiveWindow.Enabled = false;

        }

    }
}
