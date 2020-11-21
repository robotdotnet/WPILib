using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class ConstantsLowLevelNative
    {
        public ConstantsLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_GetSystemClockTicksPerMicrosecondFunc = (delegate* unmanaged[Cdecl] < System.Int32 >)loader.GetProcAddress("HAL_GetSystemClockTicksPerMicrosecond");
        }

        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetSystemClockTicksPerMicrosecondFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetSystemClockTicksPerMicrosecond()
        {
            return HAL_GetSystemClockTicksPerMicrosecondFunc();
        }



    }
}
