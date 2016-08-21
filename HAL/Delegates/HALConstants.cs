using System;
using System.Runtime.InteropServices;
// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALConstants
    {
        static HALConstants()
        {
            HAL.Initialize();
        }

        public delegate int HAL_GetSystemClockTicksPerMicrosecondDelegate();
        public static HAL_GetSystemClockTicksPerMicrosecondDelegate HAL_GetSystemClockTicksPerMicrosecond;
    }
}

