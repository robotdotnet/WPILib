using WPIHal.Natives;

namespace WPILib;

public static class RobotRunner<T> where T : RobotBase, new()
{
    private static void RunRobot()
    {
        Console.WriteLine("********** Robot program starting **********");

        T robot = new();
    }

    public static void StartRobot()
    {
        if (!HalBase.Initialize(500, 0))
        {
            throw new InvalidOperationException("Failed to initialize. Terminating");
        }

        RunRobot();
    }
}
