using System;
using System.Collections.Generic;
using System.Text;

namespace Hal
{
    public enum EncoderIndexingType
    {
        ResetWhileHigh,
        ResetWhileLow,
        ResetOnFallingEdge,
        ResetOnRisingEdge
    }

    public enum EncoderEncodingType
    {
        k1x,
        k2x,
        k4x
    }
}
