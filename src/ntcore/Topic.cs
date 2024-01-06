namespace NetworkTables;

public class Topic
{
    internal Topic(NetworkTableInstance inst, int handle)
    {
        Handle = handle;
        Instance = inst;
    }

    public bool IsValid => Handle != 0;

    public int Handle { get; }

    public NetworkTableInstance Instance { get; }
}
