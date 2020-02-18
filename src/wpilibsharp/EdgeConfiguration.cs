using System;

namespace WPILib
{
    [Flags]
#pragma warning disable CA1714 // Flags enums should have plural names
    public enum EdgeConfiguration
#pragma warning restore CA1714 // Flags enums should have plural names
    {
        kNone = 0x0,
        kRisingEdge = 0x1,
        kFallingEdge = 0x10,
        kBothEdges = kRisingEdge | kFallingEdge
    }
}
