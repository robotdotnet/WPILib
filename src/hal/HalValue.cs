using System.Runtime.InteropServices;

namespace WPIHal;

public enum HalType
{
#pragma warning disable CA1720 // Identifier contains type name
    Unassigned = 0x0,
    Boolean = 0x1,
    Double = 0x2,
    Enum = 0x4,
    Int = 0x8,
    Long = 0x10,
#pragma warning restore CA1720 // Identifier contains type name
}

[StructLayout(LayoutKind.Sequential)]
public struct HalValue
{
    [StructLayout(LayoutKind.Explicit)]
    public struct ValueUnion
    {
        [FieldOffset(0)]
        public int vBoolean;
        [FieldOffset(0)]
        public int vEnum;
        [FieldOffset(0)]
        public int vInt;
        [FieldOffset(0)]
        public long vLong;
        [FieldOffset(0)]
        public double vDouble;
    }

    public ValueUnion Data;
    public HalType Type;

    public static HalValue MakeBoolean(bool value)
    {
        HalValue hValue = new()
        {
            Type = HalType.Boolean
        };
        hValue.Data.vBoolean = value ? 1 : 0;
        return hValue;
    }

    public static HalValue MakeEnum(int value)
    {
        HalValue hValue = new()
        {
            Type = HalType.Enum
        };
        hValue.Data.vEnum = value;
        return hValue;
    }

    public static HalValue MakeInt(int value)
    {
        HalValue hValue = new()
        {
            Type = HalType.Int
        };
        hValue.Data.vInt = value;
        return hValue;
    }

    public static HalValue MakeLong(long value)
    {
        HalValue hValue = new()
        {
            Type = HalType.Long
        };
        hValue.Data.vLong = value;
        return hValue;
    }

    public static HalValue MakeDouble(double value)
    {
        HalValue hValue = new()
        {
            Type = HalType.Double
        };
        hValue.Data.vDouble = value;
        return hValue;
    }
}
