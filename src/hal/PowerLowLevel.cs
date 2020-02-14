
using Hal.Natives;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(IPower))]
    public static unsafe class PowerLowLevel
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IPower lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public static int GetUserActive3V3()
        {
            return lowLevel.HAL_GetUserActive3V3();
        }

        public static int GetUserActive5V()
        {
            return lowLevel.HAL_GetUserActive5V();
        }

        public static int GetUserActive6V()
        {
            return lowLevel.HAL_GetUserActive6V();
        }

        public static double GetUserCurrent3V3()
        {
            return lowLevel.HAL_GetUserCurrent3V3();
        }

        public static double GetUserCurrent5V()
        {
            return lowLevel.HAL_GetUserCurrent5V();
        }

        public static double GetUserCurrent6V()
        {
            return lowLevel.HAL_GetUserCurrent6V();
        }

        public static int GetUserCurrentFaults3V3()
        {
            return lowLevel.HAL_GetUserCurrentFaults3V3();
        }

        public static int GetUserCurrentFaults5V()
        {
            return lowLevel.HAL_GetUserCurrentFaults5V();
        }

        public static int GetUserCurrentFaults6V()
        {
            return lowLevel.HAL_GetUserCurrentFaults6V();
        }

        public static double GetUserVoltage3V3()
        {
            return lowLevel.HAL_GetUserVoltage3V3();
        }

        public static double GetUserVoltage5V()
        {
            return lowLevel.HAL_GetUserVoltage5V();
        }

        public static double GetUserVoltage6V()
        {
            return lowLevel.HAL_GetUserVoltage6V();
        }

        public static double GetVinCurrent()
        {
            return lowLevel.HAL_GetVinCurrent();
        }

        public static double GetVinVoltage()
        {
            return lowLevel.HAL_GetVinVoltage();
        }

    }
}
