using System.Runtime.InteropServices;
using NetworkTables.Natives;

namespace NetworkTables;

[StructLayout(LayoutKind.Auto)]
public readonly ref partial struct RefNetworkTableValue
{
    /**
 * Get the creation time of the value in local time.
 *
 * @return The time, in the units returned by NtCore.Now().
 */
    public long Time { get; }

    /*
     * Type Checkers
     */

    /**
     * Determine if entry value contains a value or is unassigned.
     *
     * @return True if the entry value contains a value.
     */
    public bool IsValid => Type != NetworkTableType.Unassigned;

    internal RefNetworkTableValue(NetworkTableType type) : this(type, NtCore.Now())
    {

    }

    internal RefNetworkTableValue(NetworkTableType type, long time)
    {
        Type = type;
        Time = time;
    }

    public static RefNetworkTableValue MakeUnassigned()
    {
        return new RefNetworkTableValue(NetworkTableType.Unassigned);
    }

    public static RefNetworkTableValue MakeUnassigned(long time)
    {
        return new RefNetworkTableValue(NetworkTableType.Unassigned, time);
    }

    // TODO Equals and HashCode

    /**
     * Get the data type.
     *
     * @return The type.
     */
    public NetworkTableType Type { get; }

    internal readonly ValueStorage m_structValue;

    [StructLayout(LayoutKind.Explicit)]
    internal readonly struct ValueStorage
    {
        public ValueStorage(bool value)
        {
            boolValue = value;
        }

        public ValueStorage(long value)
        {
            longValue = value;
        }

        public ValueStorage(float value)
        {
            floatValue = value;
        }

        public ValueStorage(double value)
        {
            doubleValue = value;
        }

        [FieldOffset(0)]
        public readonly bool boolValue;
        [FieldOffset(0)]
        public readonly long longValue;
        [FieldOffset(0)]
        public readonly float floatValue;
        [FieldOffset(0)]
        public readonly double doubleValue;
    }
}
