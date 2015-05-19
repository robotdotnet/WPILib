using System;
using System.Linq;
using System.Reflection;

namespace HAL_Base
{
    public enum AnalogTriggerType
    {
        /// kInWindow -> 0
        InWindow = 0,

        /// kState -> 1
        State = 1,

        /// kRisingPulse -> 2
        RisingPulse = 2,

        /// kFallingPulse -> 3
        FallingPulse = 3,
    }

    public partial class HALAnalog
    {
        
    }
}
