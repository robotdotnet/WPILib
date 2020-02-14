﻿using System;

namespace WPILib
{
    [Flags]
    public enum EdgeConfiguration
    {
        kNone = 0x0,
        kRisingEdge = 0x1,
        kFallingEdge = 0x10,
        kBothEdges = kRisingEdge | kFallingEdge
    }
}