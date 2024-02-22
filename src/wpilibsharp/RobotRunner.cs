using WPIHal.Natives;

namespace WPILib;

public static class RobotRunner
{
    private static void RunRobot<T>() where T : RobotBase, new()
    {
        Console.WriteLine("********** Robot program starting **********");

        T robot = new();
    }

    public static void StartRobot<T>() where T : RobotBase, new()
    {
        InitializeHAL();

        RunRobot<T>();
    }

    public static void InitializeHAL()
    {
        if (!HalBase.Initialize(500, 0))
        {
            throw new HalInitializationException("Failed to initialize. Terminating");
        }
    }
}
