using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using NetworkTables.Natives;
using WPIUtil.Marshal;

namespace NetworkTables;

[NativeMarshalling(typeof(NetworkTableValueMarshaller))]
[StructLayout(LayoutKind.Auto)]
public readonly partial struct NetworkTableValue : INativeArrayFree<NetworkTableValueMarshaller.NativeNetworkTableValue>,
                                                   IEquatable<NetworkTableValue>
{
    public static unsafe void FreeArray(NetworkTableValueMarshaller.NativeNetworkTableValue* array, int len)
    {
        NtCore.DisposeValueArray(array, (nuint)len);
    }

    /// <summary>
    /// Gets if entry value contains a string.
    /// </summary>
    public bool IsString => Type == NetworkTableType.String;

    /// <summary>
    /// Gets if entry value contains a string.
    /// </summary>
    public bool IsStringArray => Type == NetworkTableType.StringArray;

    internal NetworkTableValue(NetworkTableType type, object? obj, long time, long serverTime)
    {
        Type = type;
        Time = time;
        ServerTime = serverTime;
        m_objectValue = obj;
    }

    internal NetworkTableValue(NetworkTableType type, bool value) : this(type, null, NtCore.Now(), 1)
    {
        m_structValue = new(value);
    }

    internal NetworkTableValue(NetworkTableType type, long value) : this(type, null, NtCore.Now(), 1)
    {
        m_structValue = new(value);
    }

    internal NetworkTableValue(NetworkTableType type, float value) : this(type, null, NtCore.Now(), 1)
    {
        m_structValue = new(value);
    }

    internal NetworkTableValue(NetworkTableType type, double value) : this(type, null, NtCore.Now(), 1)
    {
        m_structValue = new(value);
    }

    internal NetworkTableValue(NetworkTableType type, object value) : this(type, value, NtCore.Now(), 1) { }

    internal NetworkTableValue(NetworkTableType type, bool value, long time) : this(type, null, time, 1)
    {
        m_structValue = new(value);
    }

    internal NetworkTableValue(NetworkTableType type, long value, long time) : this(type, null, time, 1)
    {
        m_structValue = new(value);
    }

    internal NetworkTableValue(NetworkTableType type, float value, long time) : this(type, null, time, 1)
    {
        m_structValue = new(value);
    }

    internal NetworkTableValue(NetworkTableType type, double value, long time) : this(type, null, time, 1)
    {
        m_structValue = new(value);
    }

    internal NetworkTableValue(NetworkTableType type, object value, long time) : this(type, value, time, 1)
    {
    }

    /**
 * Get the creation time of the value in local time.
 *
 * @return The time, in the units returned by NtCore.Now().
 */
    public long Time { get; }

    /**
     * Get the creation time of the value in server time.
     *
     * @return The server time.
     */
    public long ServerTime { get; }

    /*
     * Type Checkers
     */

    /**
     * Determine if entry value contains a value or is unassigned.
     *
     * @return True if the entry value contains a value.
     */
    public bool IsValid => Type != NetworkTableType.Unassigned;

    /// <summary>
    /// Get the string value.
    /// </summary>
    /// <returns>The string value.</returns>
    /// <exception cref="InvalidCastException">Thrown if the entry value is not of string type.</exception>
    public string GetString()
    {
        if (Type != NetworkTableType.String)
        {
            throw new InvalidCastException($"cannot convert {Type} to string");
        }
        return m_objectValue as string ?? "";
    }

    /// <summary>
    /// Get the string[] value.
    /// </summary>
    /// <returns>The string[] value.</returns>
    /// <exception cref="InvalidCastException">Thrown if the entry value is not of string[] type.</exception>
    public string[] GetStringArray()
    {
        if (Type != NetworkTableType.StringArray)
        {
            throw new InvalidCastException($"cannot convert {Type} to string[]");
        }
        return m_objectValue as string[] ?? [];
    }

    public object? Value
    {
        get
        {
            if (m_objectValue is not null)
            {
                return m_objectValue;
            }

            if (Type == NetworkTableType.Boolean)
            {
                bool ret = m_structValue.boolValue;
                return ret;
            }
            else if (Type == NetworkTableType.Double)
            {
                double ret = m_structValue.doubleValue;
                return ret;
            }
            else if (Type == NetworkTableType.Integer)
            {
                long ret = m_structValue.longValue;
                return ret;
            }
            else if (Type == NetworkTableType.Float)
            {
                float ret = m_structValue.floatValue;
                return ret;
            }

            return null;
        }
    }

    public static NetworkTableValue MakeUnassigned()
    {
        return new NetworkTableValue(NetworkTableType.Unassigned, null, NtCore.Now(), 1);
    }

    public static NetworkTableValue MakeUnassigned(long time)
    {
        return new NetworkTableValue(NetworkTableType.Unassigned, null, time, 1);
    }

    /// <summary>
    /// Creates a string value.
    /// </summary>
    /// <param name="value">the value</param>
    /// <returns>The entry value</returns>
    public static NetworkTableValue MakeString(string value)
    {
        return new NetworkTableValue(NetworkTableType.String, value);
    }

    /// <summary>
    /// Creates a string value.
    /// </summary>
    /// <param name="value">the value</param>
    /// <param name="time">the creation time to use (instead of the current time)</param>
    /// <returns>The entry value</returns>
    public static NetworkTableValue MakeString(string value, long time)
    {
        return new NetworkTableValue(NetworkTableType.String, value, time);
    }

    /// <summary>
    /// Creates a string[] value.
    /// </summary>
    /// <param name="value">the value</param>
    /// <returns>The entry value</returns>
    public static NetworkTableValue MakeStringArray(string[] value)
    {
        return new NetworkTableValue(NetworkTableType.StringArray, value);
    }

    /// <summary>
    /// Creates a string[] value.
    /// </summary>
    /// <param name="value">the value</param>
    /// <param name="time">the creation time to use (instead of the current time)</param>
    /// <returns>The entry value</returns>
    public static NetworkTableValue MakeStringArray(string[] value, long time)
    {
        return new NetworkTableValue(NetworkTableType.StringArray, value, time);
    }

    public override int GetHashCode()
    {
        int objectHash = Type switch
        {
            NetworkTableType.Double => m_structValue.doubleValue.GetHashCode(),
            NetworkTableType.Integer => m_structValue.longValue.GetHashCode(),
            NetworkTableType.Float => m_structValue.floatValue.GetHashCode(),
            NetworkTableType.Boolean => m_structValue.boolValue.GetHashCode(),
            _ => m_objectValue?.GetHashCode() ?? 0
        };
        return HashCode.Combine(Type, objectHash);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is NetworkTableValue value)
        {
            return Equals(value);
        }
        return false;
    }

    public bool Equals(NetworkTableValue other)
    {
        return Type == other.Type && Type switch
        {
            NetworkTableType.Unassigned => true,
            NetworkTableType.Boolean => m_structValue.boolValue == other.m_structValue.boolValue,
            NetworkTableType.Double => m_structValue.doubleValue == other.m_structValue.doubleValue,
            NetworkTableType.String => m_objectValue == other.m_objectValue,
            NetworkTableType.Raw => m_objectValue is not null && other.m_objectValue is not null && ((byte[])m_objectValue).AsSpan().SequenceEqual(((byte[])other.m_objectValue).AsSpan()),
            NetworkTableType.BooleanArray => m_objectValue is not null && other.m_objectValue is not null && ((bool[])m_objectValue).AsSpan().SequenceEqual(((bool[])other.m_objectValue).AsSpan()),
            NetworkTableType.DoubleArray => m_objectValue is not null && other.m_objectValue is not null && ((double[])m_objectValue).AsSpan().SequenceEqual(((double[])other.m_objectValue).AsSpan()),
            NetworkTableType.StringArray => m_objectValue is not null && other.m_objectValue is not null && ((string[])m_objectValue).AsSpan().SequenceEqual(((string[])other.m_objectValue).AsSpan()),
            NetworkTableType.Integer => m_structValue.longValue == other.m_structValue.longValue,
            NetworkTableType.Float => m_structValue.floatValue == other.m_structValue.floatValue,
            NetworkTableType.IntegerArray => m_objectValue is not null && other.m_objectValue is not null && ((long[])m_objectValue).AsSpan().SequenceEqual(((long[])other.m_objectValue).AsSpan()),
            NetworkTableType.FloatArray => m_objectValue is not null && other.m_objectValue is not null && ((float[])m_objectValue).AsSpan().SequenceEqual(((float[])other.m_objectValue).AsSpan()),
            _ => false,
        };
    }

    public static bool operator ==(NetworkTableValue? left, NetworkTableValue? right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(NetworkTableValue? left, NetworkTableValue? right)
    {
        return !(left == right);
    }

    /**
     * Get the data type.
     *
     * @return The type.
     */
    public NetworkTableType Type { get; }

    internal readonly object? m_objectValue;
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

    public static implicit operator RefNetworkTableValue(in NetworkTableValue value)
    {
        return value.Type switch
        {
            NetworkTableType.Boolean => RefNetworkTableValue.MakeBoolean(value.m_structValue.boolValue, value.Time),
            NetworkTableType.Double => RefNetworkTableValue.MakeDouble(value.m_structValue.doubleValue, value.Time),
            NetworkTableType.String => RefNetworkTableValue.MakeString((string)value.m_objectValue!, value.Time),
            NetworkTableType.Raw => RefNetworkTableValue.MakeRaw((byte[])value.m_objectValue!, value.Time),
            NetworkTableType.BooleanArray => RefNetworkTableValue.MakeBooleanArray((bool[])value.m_objectValue!, value.Time),
            NetworkTableType.DoubleArray => RefNetworkTableValue.MakeDoubleArray((double[])value.m_objectValue!, value.Time),
            NetworkTableType.StringArray => RefNetworkTableValue.MakeStringArray((string[])value.m_objectValue!, value.Time),
            NetworkTableType.Integer => RefNetworkTableValue.MakeInteger(value.m_structValue.longValue, value.Time),
            NetworkTableType.Float => RefNetworkTableValue.MakeFloat(value.m_structValue.floatValue, value.Time),
            NetworkTableType.IntegerArray => RefNetworkTableValue.MakeIntegerArray((long[])value.m_objectValue!, value.Time),
            NetworkTableType.FloatArray => RefNetworkTableValue.MakeFloatArray((float[])value.m_objectValue!, value.Time),
            _ => RefNetworkTableValue.MakeUnassigned(value.Time),
        };
    }

    internal unsafe NetworkTableValue(in NetworkTableValueMarshaller.NativeNetworkTableValue value)
    {
        Time = value.lastChange;
        ServerTime = value.serverTime;
        Type = value.type;

        switch (Type)
        {
            case NetworkTableType.Boolean:
                m_structValue = new(value.data.valueBoolean != 0);
                break;
            case NetworkTableType.Integer:
                m_structValue = new(value.data.valueInt);
                break;
            case NetworkTableType.Float:
                m_structValue = new(value.data.valueFloat);
                break;
            case NetworkTableType.Double:
                m_structValue = new(value.data.valueDouble);
                break;
            case NetworkTableType.String:
                m_objectValue = value.data.valueString.ConvertToString();
                break;
            case NetworkTableType.Raw:
                byte[] bytes = new byte[checked((int)value.data.valueRaw.size)];
                new ReadOnlySpan<byte>(value.data.valueRaw.data, bytes.Length).CopyTo(bytes);
                m_objectValue = bytes;
                break;
            case NetworkTableType.BooleanArray:
                bool[] boolArr = new bool[checked((int)value.data.arrBoolean.size)];
                for (int i = 0; i < boolArr.Length; i++)
                {
                    boolArr[i] = value.data.arrBoolean.arr[i] != 0;
                }
                m_objectValue = boolArr;
                break;
            case NetworkTableType.IntegerArray:
                long[] longArr = new long[checked((int)value.data.arrInt.size)];
                new ReadOnlySpan<long>(value.data.arrInt.arr, longArr.Length).CopyTo(longArr);
                m_objectValue = longArr;
                break;
            case NetworkTableType.FloatArray:
                float[] floatArr = new float[checked((int)value.data.arrFloat.size)];
                new ReadOnlySpan<float>(value.data.arrFloat.arr, floatArr.Length).CopyTo(floatArr);
                m_objectValue = floatArr;
                break;
            case NetworkTableType.DoubleArray:
                double[] doubleArr = new double[checked((int)value.data.arrDouble.size)];
                new ReadOnlySpan<double>(value.data.arrDouble.arr, doubleArr.Length).CopyTo(doubleArr);
                m_objectValue = doubleArr;
                break;
            case NetworkTableType.StringArray:
                string[] stringArr = new string[checked((int)value.data.arrString.size)];
                for (int i = 0; i < stringArr.Length; i++)
                {
                    stringArr[i] = value.data.arrString.arr[i].ConvertToString();
                }
                m_objectValue = stringArr;
                break;
        }
    }
}
