using System;
using System.Runtime.InteropServices;

namespace HAL.SimulatorHAL
{
    internal static class PortConverters
    {
        
        internal static int GetTalonSRX(IntPtr ptr)
        {
            return (int) ptr - 1;
        }
    }
}
