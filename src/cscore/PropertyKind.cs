namespace CsCore;

[Flags]
public enum PropertyKind
{
    None = 0,
    Boolean = 1,
#pragma warning disable CA1720 // Identifier contains type name
    Integer = 2,
    String = 4,
#pragma warning restore CA1720 // Identifier contains type name
    Enum = 8,
}
