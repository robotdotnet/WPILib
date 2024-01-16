namespace WPIUtil.Serialization.Struct;

public enum StructFieldType
{
    Bool,
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
    Struct
}

public record struct StructFieldTypeInfo(string Name, bool IsInt, bool IsUint, int Size);

public static class StructFieldTypeExtensions
{
    public static StructFieldTypeInfo GetFieldTypeInfo(this StructFieldType type)
    {
#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
        return type switch
        {
            StructFieldType.Bool => new("bool", false, false, 1),
            StructFieldType.Char => new("char", false, false, 1),
            StructFieldType.Int8 => new("int8", true, false, 1),
            StructFieldType.Int16 => new("int16", true, false, 2),
            StructFieldType.Int32 => new("int32", true, false, 4),
            StructFieldType.Int64 => new("int64", true, false, 8),
            StructFieldType.Uint8 => new("int8", true, true, 1),
            StructFieldType.Uint16 => new("int16", true, true, 2),
            StructFieldType.Uint32 => new("int32", true, true, 4),
            StructFieldType.Uint64 => new("int64", true, true, 8),
            StructFieldType.Float => new("float", false, false, 4),
            StructFieldType.Double => new("double", false, false, 8),
            StructFieldType.Struct => new("struct", false, false, 0),
        };
#pragma warning restore CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
    }
}
