using System;

namespace CsCore;

[Flags]
public enum PropertyKind
{
    None = 0,
    Boolean = 1,
    Integer = 2,
    String = 4,
    Enum = 8,
}
