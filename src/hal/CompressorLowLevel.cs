
using Hal.Natives;
using WPIUtil.NativeUtilities;

namespace Hal
{

    public static unsafe class CompressorLowLevel
    {
#pragma warning disable CS0649 // Field is never assigned to
        internal static CompressorLowLevelNative lowLevel = null!;
#pragma warning restore CS0649 // Field is never assigned to

        public static int CheckModule(int module)
        {
            return lowLevel.HAL_CheckCompressorModule(module);
        }

        public static int Get(int compressorHandle)
        {
            return lowLevel.HAL_GetCompressor(compressorHandle);
        }

        public static int GetClosedLoopControl(int compressorHandle)
        {
            return lowLevel.HAL_GetCompressorClosedLoopControl(compressorHandle);
        }

        public static double GetCurrent(int compressorHandle)
        {
            return lowLevel.HAL_GetCompressorCurrent(compressorHandle);
        }

        public static int GetCurrentTooHighFault(int compressorHandle)
        {
            return lowLevel.HAL_GetCompressorCurrentTooHighFault(compressorHandle);
        }

        public static int GetCurrentTooHighStickyFault(int compressorHandle)
        {
            return lowLevel.HAL_GetCompressorCurrentTooHighStickyFault(compressorHandle);
        }

        public static int GetNotConnectedFault(int compressorHandle)
        {
            return lowLevel.HAL_GetCompressorNotConnectedFault(compressorHandle);
        }

        public static int GetNotConnectedStickyFault(int compressorHandle)
        {
            return lowLevel.HAL_GetCompressorNotConnectedStickyFault(compressorHandle);
        }

        public static int GetPressureSwitch(int compressorHandle)
        {
            return lowLevel.HAL_GetCompressorPressureSwitch(compressorHandle);
        }

        public static int GetShortedFault(int compressorHandle)
        {
            return lowLevel.HAL_GetCompressorShortedFault(compressorHandle);
        }

        public static int GetShortedStickyFault(int compressorHandle)
        {
            return lowLevel.HAL_GetCompressorShortedStickyFault(compressorHandle);
        }

        public static int Initialize(int module)
        {
            return lowLevel.HAL_InitializeCompressor(module);
        }

        public static void SetClosedLoopControl(int compressorHandle, int value)
        {
            lowLevel.HAL_SetCompressorClosedLoopControl(compressorHandle, value);
        }

    }
}
