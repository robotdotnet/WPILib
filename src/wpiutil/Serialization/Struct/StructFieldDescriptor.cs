namespace WPIUtil.Serialization.Struct;

public sealed class StructFieldDescriptor
{
    private static int ToBitWidth(int size, int bitWidth)
    {
        if (bitWidth == 0)
        {
            return size * 8;
        }
        else
        {
            return bitWidth;
        }
    }

    private static long ToBitMask(int size, int bitWidth)
    {
        if (size == 0)
        {
            return 0;
        }
        else
        {
            return -1L >>> (64 - ToBitWidth(size, bitWidth));
        }
    }

    internal StructFieldDescriptor(StructDescriptor parent,
                                   string name,
                                   StructFieldTypeInfo type,
                                   int size,
                                   int arraySize,
                                   int bitWidth,
                                   Dictionary<string, long>? enumValues,
                                   StructDescriptor? structDesc)
    {
        Parent = parent;
        Name = name;
        Type = type;
        Size = size;
        ArraySize = arraySize;
        Struct = structDesc;
        EnumValues = enumValues;
        BitWidth = ToBitWidth(size, bitWidth);
        BitMask = ToBitMask(size, bitWidth);
    }

    public long BitMask { get; }

    public Dictionary<string, long>? EnumValues { get; }

    public string Name { get; }

    public int Offset { get; internal set; }
    public int Size { get; internal set; }
    public int ArraySize { get; }
    public bool IsBitField { get; }
    public int BitWidth { get; }
    public int BitShift { get; internal set; }
    public StructFieldTypeInfo Type { get; }
    public StructDescriptor? Struct { get; }

    public StructDescriptor Parent { get; }

    public bool IsInt => Type.IsInt;
    public bool IsUint => Type.IsUint;

    public bool IsArray => ArraySize > 1;

    public bool HasEnum => EnumValues is not null;

#pragma warning disable CA1822 // Mark members as static
    public long UintMin => 0;
#pragma warning restore CA1822 // Mark members as static
    public long UintMax => BitMask;
    public long IntMin => (-(BitMask >> 1)) - 1;
    public long IntMax => BitMask >> 1;
}
