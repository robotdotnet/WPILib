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
        HalBase.Initialize();
    }
}
