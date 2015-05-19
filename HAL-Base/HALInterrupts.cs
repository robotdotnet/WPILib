
using System;
using System.Linq;
using System.Reflection;

namespace HAL_Base
{
    /// Return Type: void
    ///interruptAssertedMask: unsigned int
    ///param: void*
    public delegate void InterruptHandlerFunction(uint interruptAssertedMask, System.IntPtr param);

    public partial class HALInterrupts
    {
        
    }
}
