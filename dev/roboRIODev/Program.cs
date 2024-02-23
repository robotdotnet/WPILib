using System.Runtime.CompilerServices;
using Stereologue;
using WPILib;

namespace TestRobot;

internal class Program
{
    private static void Main(string[] args)
    {
        RobotRunner.StartRobot<Robot>();
    }
}

internal static class RobotInitializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        // Force load the HAL, because we can't guarantee it won't be
        // accidentally used too early later
        RobotRunner.InitializeHAL();
    }
}

[GenerateLog]
public partial class MyClass
{
    [Log]
    public int ToLog2;
}
