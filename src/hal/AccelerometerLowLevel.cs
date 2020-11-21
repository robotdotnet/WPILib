
using Hal.Natives;
using WPIUtil.ILGeneration;
using WPIUtil.NativeUtilities;

namespace Hal
{

    public static unsafe class AccelerometerLowLevel
    {
        internal static AccelerometerLowLevelNative lowLevel = null!;

        internal static void InitializeNatives(IFunctionPointerLoader loader)
        {
            lowLevel = new(loader);
        }

        public static double GetX()
        {
            return lowLevel.HAL_GetAccelerometerX();
        }

        public static double GetY()
        {
            return lowLevel.HAL_GetAccelerometerY();
        }

        public static double GetZ()
        {
            return lowLevel.HAL_GetAccelerometerZ();
        }

        public static void SetActive(bool active)
        {
            lowLevel.HAL_SetAccelerometerActive(active ? 1 : 0);
        }

        public static void SetRange(AccelerometerRange range)
        {
            lowLevel.HAL_SetAccelerometerRange(range);
        }

    }
}
