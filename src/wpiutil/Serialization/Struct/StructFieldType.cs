using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WPIUtil.Serialization.Struct;

public enum StructFieldType
{
    Bool,
#pragma warning disable CA1720 // Identifier contains type name
    Char,
    Int8,
    Int16,
    Int32,
    Int64,
    Uint8,
    Uint16,
    Uint32,
    Uint64,
    Float,
    Double,
#pragma warning restore CA1720 // Identifier contains type name
    Struct
}

public record struct StructFieldTypeInfo(string Name, bool IsInt, bool IsUint, int Size, StructFieldType Type);

public static class StructFieldTypeHelpers
{
    public static StructFieldTypeInfo GetFieldTypeInfo(this StructFieldType type)
    {
#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
        return type switch
        {
            StructFieldType.Bool => new("bool", false, false, 1, StructFieldType.Bool),
            StructFieldType.Char => new("char", false, false, 1, StructFieldType.Char),
            StructFieldType.Int8 => new("int8", true, false, 1, StructFieldType.Int8),
            StructFieldType.Int16 => new("int16", true, false, 2, StructFieldType.Int16),
            StructFieldType.Int32 => new("int32", true, false, 4, StructFieldType.Int32),
            StructFieldType.Int64 => new("int64", true, false, 8, StructFieldType.Int64),
            StructFieldType.Uint8 => new("uint8", true, true, 1, StructFieldType.Uint8),
            StructFieldType.Uint16 => new("uint16", true, true, 2, StructFieldType.Uint16),
            StructFieldType.Uint32 => new("uint32", true, true, 4, StructFieldType.Uint32),
            StructFieldType.Uint64 => new("uint64", true, true, 8, StructFieldType.Uint64),
            StructFieldType.Float => new("float", false, false, 4, StructFieldType.Float),
            StructFieldType.Double => new("double", false, false, 8, StructFieldType.Double),
            StructFieldType.Struct => new("struct", false, false, 0, StructFieldType.Struct),
        };
#pragma warning restore CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
    }

    private static readonly Dictionary<string, StructFieldTypeInfo> typeInfos = new()
    {
        ["bool"] = StructFieldType.Bool.GetFieldTypeInfo(),
        ["char"] = StructFieldType.Char.GetFieldTypeInfo(),
        ["int8"] = StructFieldType.Int8.GetFieldTypeInfo(),
        ["int16"] = StructFieldType.Int16.GetFieldTypeInfo(),
        ["int32"] = StructFieldType.Int32.GetFieldTypeInfo(),
        ["int64"] = StructFieldType.Int64.GetFieldTypeInfo(),
        ["uint8"] = StructFieldType.Uint8.GetFieldTypeInfo(),
        ["uint16"] = StructFieldType.Uint16.GetFieldTypeInfo(),
        ["uint32"] = StructFieldType.Uint32.GetFieldTypeInfo(),
        ["uint64"] = StructFieldType.Uint64.GetFieldTypeInfo(),
        ["float"] = StructFieldType.Float.GetFieldTypeInfo(),
        ["double"] = StructFieldType.Double.GetFieldTypeInfo(),
        ["float32"] = StructFieldType.Float.GetFieldTypeInfo(),
        ["double64"] = StructFieldType.Double.GetFieldTypeInfo(),
        ["struct"] = StructFieldType.Struct.GetFieldTypeInfo(),
    };

    private static readonly StructFieldTypeInfo structTypeInfo = StructFieldType.Struct.GetFieldTypeInfo();

    public static ref readonly StructFieldTypeInfo GetTypeInfoForString(string name)
    {
        ref StructFieldTypeInfo info = ref CollectionsMarshal.GetValueRefOrNullRef(typeInfos, name);
        if (Unsafe.IsNullRef(ref info))
        {
            return ref structTypeInfo;
        }
        return ref info;
    }
}
