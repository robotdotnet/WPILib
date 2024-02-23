namespace NetworkTables;

[Flags]
public enum NetworkTableType : uint
{
    Unassigned = 0,
    Boolean = 0x01,
#pragma warning disable CA1720 // Identifier contains type name
    Double = 0x02,
    String = 0x04,
    Raw = 0x08,
    BooleanArray = 0x10,
    DoubleArray = 0x20,
    StringArray = 0x40,
    Rpc = 0x80,
    Integer = 0x100,
    Float = 0x200,
#pragma warning restore CA1720 // Identifier contains type name
    IntegerArray = 0x400,
    FloatArray = 0x800
}

public static class NetworkTableTypeHelpers
{
    public static NetworkTableType GetFromString(string typeString)
    {
        return typeString switch
        {
            "boolean" => NetworkTableType.Boolean,
            "double" => NetworkTableType.Double,
            "float" => NetworkTableType.Float,
            "int" => NetworkTableType.Integer,
            "string" or "json" => NetworkTableType.String,
            "boolean[]" => NetworkTableType.BooleanArray,
            "double[]" => NetworkTableType.DoubleArray,
            "float[]" => NetworkTableType.FloatArray,
            "int[]" => NetworkTableType.IntegerArray,
            "string[]" => NetworkTableType.StringArray,
            "" => NetworkTableType.Unassigned,
            _ => NetworkTableType.Raw,
        };
    }
}
