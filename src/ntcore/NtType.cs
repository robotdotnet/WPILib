using System;

namespace NetworkTables;

[Flags]
public enum NtType : uint
{
    Unassigned = 0,
    Boolean = 0x01,
    Double = 0x02,
    String = 0x04,
    Raw = 0x08,
    BooleanArray = 0x10,
    DoubleArray = 0x20,
    StringArray = 0x40,
    Rpc = 0x80,
    Integer = 0x100,
    Float = 0x200,
    IntegerArray = 0x400,
    FloatArray = 0x800
}
