using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System;

namespace HAL_Base
{
    public enum Mode
    {
        /// kTwoPulse -> 0
        TwoPulse = 0,

        /// kSemiperiod -> 1
        Semiperiod = 1,

        /// kPulseLength -> 2
        PulseLength = 2,

        /// kExternalDirection -> 3
        ExternalDirection = 3,
    }

    public partial class HALDigital
    {
        
    }
}
