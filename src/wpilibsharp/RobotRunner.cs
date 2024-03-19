using WPIHal.Natives;

namespace WPILib;

public static class RobotRunner
{
    private static readonly object m_runMutex = new();
    private static RobotBase? m_robotCopy;
    private static bool m_suppressExitWarning;

    private static void RunRobot<T>() where T : RobotBase, new()
    {
        Console.WriteLine("********** Robot program starting **********");

        T robot;
        try
        {
            robot = new T();
        }
        catch (Exception ex)
        {
            DriverStation.ReportError($"Unhandled exception instantiating robot {typeof(T).Name} {ex}", false);
            return;
        }

        lock (m_runMutex)
        {
            m_robotCopy = robot;
        }

        if (!RobotBase.IsSimulation)
        {
            string file = $"/tmp/frc_versions/FRC_Lib_Version.ini";
            try
            {
                File.WriteAllText(file, $"C# {typeof(RobotRunner).Assembly.GetName().Version}");
            }
            catch (Exception ex)
            {
                DriverStation.ReportError($"Could not write FRC_Lib_Version.ini: {ex}", false);
            }
        }

        bool errorOnExit = false;

        try
        {
            robot.StartCompetition();
        }
        catch (Exception ex)
        {
            DriverStation.ReportError($"Unhandled exception: {ex}", false);
            errorOnExit = true;
        }
        finally
        {
            bool suppressExitWarning;
            lock (m_runMutex)
            {
                suppressExitWarning = m_suppressExitWarning;
            }
            if (!suppressExitWarning)
            {
                // startCompetition never returns unless exception occurs....
                DriverStation.ReportWarning(
                    "The robot program quit unexpectedly."
                        + " This is usually due to a code error.\n"
                        + "  The above stacktrace can help determine where the error occurred.\n"
                        + "  See https://wpilib.org/stacktrace for more information.",
                    false);
                if (errorOnExit)
                {
                    DriverStation.ReportError(
                        "The StartCompetition() method (or methods called by it) should have "
                            + "handled the exception above.",
                        false);
                }
                else
                {
                    DriverStation.ReportError("Unexpected return from startCompetition() method.", false);
                }
            }
        }
    }

    public static bool SuppressExitWarning
    {
        get
        {
            lock (m_runMutex)
            {
                return m_suppressExitWarning;
            }
        }
        set
        {
            lock (m_runMutex)
            {
                m_suppressExitWarning = value;
            }
        }
    }

    public static void StartRobot<T>() where T : RobotBase, new()
    {
        InitializeHAL();

        DriverStation.RefreshData();

        // HAL Report

        if (!Notifier.SetHalThreadPriority(true, 40))
        {
            DriverStation.ReportWarning("Setting HAL Notifier RT priority to 40 failed", false);
        }

        if (HalMain.HasMain())
        {
            Thread thread = new(() =>
            {
                RunRobot<T>();
                HalMain.ExitMain();
            })
            {
                Name = "robot main",
                IsBackground = true
            };
            thread.Start();
            HalMain.RunMain();
            SuppressExitWarning = true;
            RobotBase? robot;
            lock (m_runMutex)
            {
                robot = m_robotCopy;
            }
            robot?.EndCompetition();
            thread.Join(1000);
        }
        else
        {
            RunRobot<T>();
        }

        // // On RIO, this will just terminate rather than shutting down cleanly (it's a no-op in sim).
        // // It's not worth the risk of hanging on shutdown when we want the code to restart as quickly
        // // as possible.
        // HalBase.Terminate();

        HalBase.Shutdown();

        Environment.Exit(0);
    }

    public static void InitializeHAL()
    {
        HalBase.Initialize();
    }
}
