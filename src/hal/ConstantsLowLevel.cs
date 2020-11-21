
using Hal.Natives;
using WPIUtil.ILGeneration;

namespace Hal
{

    public static unsafe class ConstantsLowLevel
    {
        internal static ConstantsLowLevelNative lowLevel = null!;

        internal static void InitializeNatives(IFunctionPointerLoader loader)
        {
            lowLevel = new(loader);
        }

        public static int GetSystemClockTicksPerMicrosecond()
        {
            return lowLevel.HAL_GetSystemClockTicksPerMicrosecond();
        }

    }
}
