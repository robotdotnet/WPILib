using System;
using System.Runtime.InteropServices;
using HAL.Base;

// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HALConstants
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALConstants.HAL_GetSystemClockTicksPerMicrosecond = HAL_GetSystemClockTicksPerMicrosecond;
        }

        public static int HAL_GetSystemClockTicksPerMicrosecond()
        {
            return 40;
        }
    }
}

