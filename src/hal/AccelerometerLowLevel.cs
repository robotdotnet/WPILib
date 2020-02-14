
using Hal.Natives;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(IAccelerometer))]
    public static unsafe class AccelerometerLowLevel
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IAccelerometer lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

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
