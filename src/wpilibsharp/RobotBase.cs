namespace WPILib;

public abstract class RobotBase
{
    public static bool IsReal => true;

    public static RuntimeType RuntimeType => RuntimeType.RoboRIO;
}
