using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using NetworkTables.Natives;

namespace NetworkTables;

[StructLayout(LayoutKind.Auto)]
[NativeMarshalling(typeof(RefNetworkTableValueMarshaller))]
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

    internal RefNetworkTableValue(ReadOnlySpan<byte> value, StringStorageType storageType)
    {
        Type = NetworkTableType.String;
        Time = NtCore.Now();
        m_structValue = new ValueStorage(storageType);
        m_byteSpan = value;
    }

    internal RefNetworkTableValue(ReadOnlySpan<byte> value, long time, StringStorageType storageType)
    {
        Type = NetworkTableType.String;
        Time = time;
        m_structValue = new ValueStorage(storageType);
        m_byteSpan = value;
    }

    /// <summary>
    /// Creates a string value.
    /// </summary>
    /// <param name="value">the value</param>
    /// <returns>The entry value</returns>
    public static RefNetworkTableValue MakeString(string value)
    {
        return new RefNetworkTableValue(MemoryMarshal.AsBytes(value.AsSpan()), StringStorageType.Char);
    }

    /// <summary>
    /// Creates a string value.
    /// </summary>
    /// <param name="value">the value</param>
    /// <param name="time">the creation time to use (instead of the current time)</param>
    /// <returns>The entry value</returns>
    public static RefNetworkTableValue MakeString(ReadOnlySpan<char> value, long time)
    {
        return new RefNetworkTableValue(MemoryMarshal.AsBytes(value), time, StringStorageType.Char);
    }

    /// <summary>
    /// Creates a string value.
    /// </summary>
    /// <param name="value">the value</param>
    /// <returns>The entry value</returns>
    public static RefNetworkTableValue MakeString(ReadOnlySpan<char> value)
    {
        return new RefNetworkTableValue(MemoryMarshal.AsBytes(value), StringStorageType.Char);
    }

    /// <summary>
    /// Creates a string value.
    /// </summary>
    /// <param name="value">the value</param>
    /// <param name="time">the creation time to use (instead of the current time)</param>
    /// <returns>The entry value</returns>
    public static RefNetworkTableValue MakeString(string value, long time)
    {
        return new RefNetworkTableValue(MemoryMarshal.AsBytes(value.AsSpan()), time, StringStorageType.Char);
    }

    /// <summary>
    /// Creates a string value from a UFT8 string.
    /// </summary>
    /// <param name="value">the value</param>
    /// <returns>The entry value</returns>
    public static RefNetworkTableValue MakeString(ReadOnlySpan<byte> value)
    {
        return new RefNetworkTableValue(value, StringStorageType.Utf8);
    }

    /// <summary>
    /// Creates a string value from a UFT8 string.
    /// </summary>
    /// <param name="value">the value</param>
    /// <param name="time">the creation time to use (instead of the current time)</param>
    /// <returns>The entry value</returns>
    public static RefNetworkTableValue MakeString(ReadOnlySpan<byte> value, long time)
    {
        return new RefNetworkTableValue(value, time, StringStorageType.Utf8);
    }

    internal RefNetworkTableValue(ReadOnlySpan<string> value)
    {
        Type = NetworkTableType.StringArray;
        Time = NtCore.Now();
        m_stringSpan = value;
    }

    internal RefNetworkTableValue(ReadOnlySpan<string> value, long time)
    {
        Type = NetworkTableType.StringArray;
        Time = time;
        m_stringSpan = value;
    }

    /// <summary>
    /// Creates a string[] value.
    /// </summary>
    /// <param name="value">the value</param>
    /// <returns>The entry value</returns>
    public static RefNetworkTableValue MakeStringArray(ReadOnlySpan<string> value)
    {
        return new RefNetworkTableValue(value);
    }

    /// <summary>
    /// Creates a string[] value.
    /// </summary>
    /// <param name="value">the value</param>
    /// <param name="time">the creation time to use (instead of the current time)</param>
    /// <returns>The entry value</returns>
    public static RefNetworkTableValue MakeStringArray(ReadOnlySpan<string> value, long time)
    {
        return new RefNetworkTableValue(value, time);
    }

    // TODO Equals and HashCode

    /**
     * Get the data type.
     *
     * @return The type.
     */
    public NetworkTableType Type { get; }

    internal readonly ValueStorage m_structValue;
    internal readonly ReadOnlySpan<string> m_stringSpan;
    internal readonly ReadOnlySpan<byte> m_byteSpan;

    internal enum StringStorageType
    {
        Char,
        Utf8
    }

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

        public ValueStorage(StringStorageType value)
        {
            stringStorageType = value;
        }

        [FieldOffset(0)]
        public readonly bool boolValue;
        [FieldOffset(0)]
        public readonly long longValue;
        [FieldOffset(0)]
        public readonly float floatValue;
        [FieldOffset(0)]
        public readonly double doubleValue;
        [FieldOffset(0)]
        public readonly StringStorageType stringStorageType;
    }
}
