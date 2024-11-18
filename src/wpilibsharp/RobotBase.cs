using NetworkTables;
using WPIHal.Natives;

namespace WPILib;

public abstract class RobotBase : IDisposable
{
    private readonly MultiSubscriber m_suball;

    protected RobotBase()
    {
        NetworkTableInstance inst = NetworkTableInstance.Default;
        MainThreadId = Environment.CurrentManagedThreadId;
        // subscribe = "" to force persistent values to propagate to local
        m_suball = new MultiSubscriber(inst, [""]);
        if (!IsSimulation)
        {
            inst.StartServer("/home/lvuser/networktables.json");
        }
        else
        {
            inst.StartServer();
        }

        // Wait for the NT server to actually start
        int count = 0;
        while (inst.GetNetworkMode().HasFlag(NetworkMode.Starting))
        {
            Thread.Sleep(10);
            count++;
            if (count > 100)
            {
                Console.Error.WriteLine("Timed out while waiting for NT server to start");
                break;
            }
        }
    }

    public static long MainThreadId { get; private set; } = -1;

    public virtual void Dispose()
    {
        GC.SuppressFinalize(this);
        m_suball.Dispose();
    }

    public static RuntimeType RuntimeType => (RuntimeType)HalBase.GetRuntimeType();

    public static bool IsSimulation => RuntimeType == RuntimeType.Simulation;

    public static bool IsReal
    {
        get
        {
            var rt = RuntimeType;
            return rt is RuntimeType.RoboRIO or RuntimeType.RoboRIO2;
        }
    }

#pragma warning disable CA1822 // Mark members as static
    public bool IsDisabled => DriverStation.IsDisabled;

    public bool IsEnabled => DriverStation.IsEnabled;

    public bool IsAutonomous => DriverStation.IsAutonomous;

    public bool IsAutonomousEnabled => DriverStation.IsAutonomousEnabled;

    public bool IsTest => DriverStation.IsTest;

    public bool IsTestEnabled => DriverStation.IsTestEnabled;

    public bool IsTeleop => DriverStation.IsTeleopEnabled;

    public bool IsTeleopEnabled => DriverStation.IsTeleopEnabled;
#pragma warning restore CA1822 // Mark members as static

    public abstract void StartCompetition();

    public abstract void EndCompetition();
}
