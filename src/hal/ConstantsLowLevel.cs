
using Hal.Natives;
using WPIUtil.NativeUtilities;

namespace Hal
{

    public static unsafe class ConstantsLowLevel
    {
#pragma warning disable CS0649 // Field is never assigned to
        internal static ConstantsLowLevelNative lowLevel = null!;
#pragma warning restore CS0649 // Field is never assigned to

        public static int GetSystemClockTicksPerMicrosecond()
        {
            return lowLevel.HAL_GetSystemClockTicksPerMicrosecond();
        }

    }
}
