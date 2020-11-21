using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
    public unsafe class ConstantsLowLevelNative
    {
        
        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetSystemClockTicksPerMicrosecondFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetSystemClockTicksPerMicrosecond()
        {
            return HAL_GetSystemClockTicksPerMicrosecondFunc();
        }



    }
}
